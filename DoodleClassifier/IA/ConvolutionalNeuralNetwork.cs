using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IA
{
    /// <summary>
    /// Réseau de neurones convolutionels.
    /// Destiné à analyser des images.
    /// </summary>
    [DataContract]
    public class ConvolutionalNeuralNetwork
    {
        /// <summary>
        /// Fonctions de pooling à appliquer dans le cadre d'un réseau convolutif.
        /// </summary>
        public enum ePooling : byte
        {
            /// <summary>
            /// Récupère la valeur maximale.
            /// </summary>
            Max,
            /// <summary>
            /// Récupère la moyenne de toutes les valeurs.
            /// </summary>
            Average,
        }
        private Random _rnd;
        [DataMember]
        private int _inputWidth;
        [DataMember]
        private int _inputHeight;

        [DataMember]
        private int _nn_NbInputs;
        [DataMember]
        private NeuralNetwork _nn;
        [DataMember]
        private List<Layer> _layers;

        [DataMember]
        public float MinInputRaw { get; private set; }
        [DataMember]
        public float MaxInputRaw { get; private set; }
        /// <summary>
        /// Récupère les layers sous forme d'array.
        /// </summary>
        public Layer[] Layers { get { return _layers.ToArray(); } }

        /// <summary>
        /// Création d'un réseau de neurones convolutif.
        /// </summary>
        /// <param name="pWidth">Largeur des images à analyser.</param>
        /// <param name="pHeight">Hauteur des images à analyser.</param>
        /// <param name="pLayers">Liste de couches de la partie convolutive.</param>
        /// <param name="pNeuralNetwork_NbHiddens">Nombre de neurones pour chaque couches cachés dans le réseau de neurones.</param>
        /// <param name="pNeuralNetwork_NbOutputs">Nombre de sorties désirées.</param>
        /// <param name="pActivation">Fonction d'activation du réseau de neurones.</param>
        public ConvolutionalNeuralNetwork(int pWidth, int pHeight, List<Layer> pLayers, int[] pNeuralNetwork_NbHiddens, int pNeuralNetwork_NbOutputs, float pLearningRate, ActivationFunctions.eActivationFunction pActivation = ActivationFunctions.eActivationFunction.Sigmoid, float pMinInputRaw = 0, float pMaxInputRaw = 1)
        {
            _rnd = new Random();
            _layers = pLayers;
            _inputWidth = pWidth;
            _inputHeight = pHeight;
            MinInputRaw = pMinInputRaw;
            MaxInputRaw = pMaxInputRaw;

            int nnInputWidth = _inputWidth;
            int nnInputHeight = _inputHeight;

            for (int idxLayer = 0; idxLayer < _layers.Count; idxLayer++)
            {
                Layer l = _layers[idxLayer];
                int maxColumns = 0;
                int maxRows = 0;
                for (int idxFilter = 0; idxFilter < l.Filters.Count; idxFilter++)
                {
                    Matrix f = l.Filters[idxFilter];
                    maxRows = (int)Math.Ceiling((double)(nnInputHeight - f.Rows + l.Padding * 2) / l.Step + 1);
                    maxColumns = (int)Math.Ceiling((double)(nnInputWidth - f.Columns + l.Padding * 2) / l.Step + 1);
                    if (idxLayer == _layers.Count - 1)
                    {
                        _nn_NbInputs += maxColumns * maxRows;
                    }
                }
                nnInputWidth = maxColumns;
                nnInputHeight = maxRows;
            }
            _nn = new NeuralNetwork(_nn_NbInputs, pNeuralNetwork_NbHiddens, pNeuralNetwork_NbOutputs, pActivation);
            _nn.LearningRate = pLearningRate;
        }

        private float Normalization(float value, int idxRow, int idxColumn)
        {
            return (-1 + (value - MinInputRaw) * (1 - -1)) / (MaxInputRaw - MinInputRaw);
        }

        /// <summary>
        /// Exécution du réseau de neurones à convolution pour reconnaitre l'image passée en entrée.
        /// </summary>
        /// <param name="pInputs">Image d'entrée.</param>
        /// <returns></returns>
        public float[] FeedForward(float[] pInputs)
        {
            float[] convOutput = ConvolutionExecute(pInputs);
            return _nn.FeedForward(convOutput);
        }

        private float[] ConvolutionExecute(float[] pInputs)
        {
            List<Matrix> O = new List<Matrix>();
            Matrix m = Matrix.FromArray(pInputs, _inputWidth);
            m.ExecuteOnMatrix(Normalization);
            O.Add(m);
            Layer l = null;
            for (int i = 0; i < _layers.Count; i++)
            {
                l = _layers[i];
                O = l.FeedForward(O);
            }
            return l.ToArray();
        }

        public Tuple<float[], List<Matrix>> Train(float[] pInputs, float[] pTarget)
        {
            float[] convOutput = ConvolutionExecute(pInputs);
            Tuple<float[], float[]> nnOutput = _nn.Train(convOutput, pTarget);
            List<float> error = nnOutput.Item2.ToList();
            Layer l = _layers[_layers.Count - 1];
            List<Matrix> lstOutputs = l.Outputs;
            List<Matrix> errors = new List<Matrix>();
            for (int i = 0; i < lstOutputs.Count; i++)
            {
                Matrix o = lstOutputs[i];
                Matrix e = Matrix.FromArray(error.GetRange(i * o.Rows * o.Columns, o.Rows * o.Columns).ToArray(), o.Columns);
                errors.Add(e);
            }

            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                l = _layers[i];
                List<Matrix> matrices = null;
                if (i > 0)
                {
                    Layer previousLayer = _layers[i - 1];
                    matrices = previousLayer.Outputs;
                }
                else
                {
                    matrices = new List<Matrix>();
                    matrices.Add(Matrix.FromArray(pInputs, _inputWidth));
                }
                errors = l.AdjustFilters(matrices, errors, _nn.LearningRate);
            }
            return new Tuple<float[], List<Matrix>> (nnOutput.Item1, errors);
        }

        [DataContract]
        public class Layer
        {
            public enum eFilterType : byte
            {
                Hazard,
                One,
                Horizontal,
                Vertical,
                DiagonalLeft,
                DiagonalRight,
            }

            private int _filtersSize;
            [DataMember]
            private List<float> _bias;
            
            [DataMember]
            public List<Matrix> Filters { get; private set; }
            [DataMember]
            public List<Matrix> Outputs { get; private set; }
            [DataMember]
            public ePooling Pooling { get; private set; }
            [DataMember]
            public ActivationFunctions.eActivationFunction ActivationFunction { get; set; }
            [DataMember]
            public int Padding { get; private set; }
            [DataMember]
            public int Step { get; private set; }

            public Layer(ePooling pPooling, int pFiltersSize, int pPadding = 0, int pStep = 1, ActivationFunctions.eActivationFunction pActivationFunction = ActivationFunctions.eActivationFunction.ReLU)
            {
                Filters = new List<Matrix>();
                _bias = new List<float>();
                Pooling = pPooling;
                _filtersSize = pFiltersSize;
                Padding = pPadding;
                Step = pStep;
                ActivationFunction = pActivationFunction;
            }

            public void AddFilter(eFilterType pFilterType, Random pRandom = null)
            {
                // Initialisation du bias
                //Matrix B = new Matrix(_filtersSize, _filtersSize);
                if (pRandom != null)
                {
                    //B.Randomize(pRandom);
                    _bias.Add((float)pRandom.NextDouble());
                }
                else
                {
                    //B.Randomize();
                    Random rnd = new Random();
                    _bias.Add((float)rnd.NextDouble());
                }
                //_bias.Add(B);

                // Initialisation du filtre
                Matrix F = new Matrix(_filtersSize, _filtersSize);

                if (pFilterType == eFilterType.Hazard)
                {
                    if (pRandom != null)
                    {
                        F.Randomize(pRandom);
                    }
                    else
                    {
                        F.Randomize();
                    }
                }
                else
                {
                    for (int r = 0; r < _filtersSize; r++)
                    {
                        for (int c = 0; c < _filtersSize; c++)
                        {
                            switch (pFilterType)
                            {
                                case eFilterType.One:
                                    F.Data[r * _filtersSize + c] = 1;
                                    break;
                                case eFilterType.Horizontal:
                                    if (r == 0)
                                    {
                                        F.Data[r * _filtersSize + c] = -1;
                                    }
                                    else if (r == _filtersSize - 1)
                                    {
                                        F.Data[r * _filtersSize + c] = 1;
                                    }
                                    else
                                    {
                                        F.Data[r * _filtersSize + c] = 0;
                                    }
                                    break;
                                case eFilterType.Vertical:
                                    if (c == 0)
                                    {
                                        F.Data[r * _filtersSize + c] = -1;
                                    }
                                    else if (c == _filtersSize - 1)
                                    {
                                        F.Data[r * _filtersSize + c] = 1;
                                    }
                                    else
                                    {
                                        F.Data[r * _filtersSize + c] = 0;
                                    }
                                    break;
                                case eFilterType.DiagonalLeft:
                                    if (r == c)
                                    {
                                        F.Data[r * _filtersSize + c] = 1;
                                    }
                                    else
                                    {
                                        F.Data[r * _filtersSize + c] = 0;
                                    }
                                    break;
                                case eFilterType.DiagonalRight:
                                    if (r == _filtersSize - c - 1)
                                    {
                                        F.Data[r * _filtersSize + c] = 1;
                                    }
                                    else
                                    {
                                        F.Data[r * _filtersSize + c] = 0;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                Filters.Add(F);
            }

            /// <summary>
            /// Applique le filtre sur la matrice d'entrée et renvoie le résultat sous forme de matrice.
            /// </summary>
            /// <param name="pInput">Matrice d'entrée.</param>
            /// <param name="pFilter">Filtre qui doit être appliqué.</param>
            /// <param name="pBias">Biais qui doit être ajouté.</param>
            /// <returns>Matrice correspondant au résultat du filtre appliqué sur la matrice d'entrée.</returns>
            private Matrix PadMatrix(Matrix pInput, Matrix pFilter, float pBias) // Matrix pBias)
            {
                if (Step > pFilter.Columns || Step > pFilter.Rows || Step < 1)
                {
                    throw new Exception("The steps should be between 1 and the filter size !");
                }
                int maxRows = (int)Math.Ceiling((double)(pInput.Rows - pFilter.Rows + Padding * 2) / Step + 1);
                int maxColumns = (int)Math.Ceiling((double)(pInput.Columns - pFilter.Columns + Padding * 2) / Step + 1);
                Matrix result = new Matrix(maxRows, maxColumns);
                int index = 0;
                for (int row = 0 - Padding; row < pInput.Rows - Padding; row += Step)
                {
                    for (int col = 0 - Padding; col < pInput.Columns - Padding; col += Step)
                    {
                        Matrix m = pInput.GetPad(pFilter.Columns, pFilter.Rows, col, row);
                        m.Multiply(pFilter);
                        m.Add(pBias);
                        float val = 0;
                        switch (Pooling)
                        {
                            case ePooling.Max:
                                val = m.Data.Max();
                                break;
                            case ePooling.Average:
                                val = m.Data.Average();
                                break;
                            default:
                                break;
                        }
                        result.Data[index] = val;
                        index++;
                    }
                }
                return result;
            }

            public List<Matrix> FeedForward(List<Matrix> pInputs)
            {
                Outputs = new List<Matrix>();
                for (int idxFilter = 0; idxFilter < Filters.Count; idxFilter++)
                {
                    Matrix F = Filters[idxFilter];
                    //Matrix B = _bias[idxFilter];
                    float B = _bias[idxFilter];
                    Matrix input = pInputs[0];
                    Matrix outMatrix = PadMatrix(input, F, B);

                    for (int i = 1; i < pInputs.Count; i++)
                    {
                        input = pInputs[i];
                        outMatrix.Add(PadMatrix(input, F, B));
                    }
                    outMatrix.Divide(pInputs.Count + 1);
                    outMatrix.ExecuteOnMatrix(ActivationFunctions.GetActivationFunction(ActivationFunction));
                    Outputs.Add(outMatrix);
                }
                return Outputs;
            }

            public List<Matrix> AdjustFilters(List<Matrix> pInputs, List<Matrix> pErrors, float pLearningRate)
            {
                List<Matrix> outputErrors = new List<Matrix>();
                for (int idxInput = 0; idxInput < pInputs.Count; idxInput++)
                {
                    Matrix i = pInputs[idxInput];
                    Matrix outputError = new Matrix(i.Rows, i.Columns);
                    for (int idxError = 0; idxError < pErrors.Count; idxError++)
                    {
                        Matrix O = Outputs[idxError];
                        Matrix error = pErrors[idxError];
                        Matrix F = Filters[idxError];
                        //Matrix B = _bias[idxError];
                        Matrix deltaFilter = new Matrix(_filtersSize, _filtersSize);
                        Matrix gradient = O.Copy();
                        gradient.ExecuteOnMatrix(ActivationFunctions.GetDerivedActivationFunction(ActivationFunction));
                        gradient.Multiply(error);
                        gradient.Multiply(pLearningRate);
                        int index = 0;
                        for (int row = 0 - Padding; row < i.Rows - Padding; row++)//= Step)
                        {
                            for (int col = 0 - Padding; col < i.Columns - Padding; col++)//= Step)
                            {
                                // TODO : vérifier
                                Matrix previous = i.GetPad(i.Columns, i.Rows, col, row);
                                //B.Add(gradient, index / (B.Columns - 1), index % (B.Columns - 1));
                                _bias[idxError] += gradient.Data.Average();
                                deltaFilter.Add(Matrix.Multiply(gradient, previous), index / (deltaFilter.Columns - 1), index % (deltaFilter.Columns - 1));
                                outputError.Add(F, row, col);
                                index++;
                            }
                        }
                        //deltaFilter.Divide(index);
                        F.Add(deltaFilter);
                    }
                    outputErrors.Add(outputError);
                }
                return outputErrors;
            }

            public float[] ToArray()
            {
                List<float> result = new List<float>();
                for (int i = 0; i < Outputs.Count; i++)
                {
                    Matrix O = Outputs[i];
                    result.AddRange(O.Data);
                }
                return result.ToArray();
            }
        }
    }
}