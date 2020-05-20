using System;
using System.Collections.Generic;

namespace EP13PPE
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Criando e atribuindo valores aos nós da rede

            List<Node> nos = new List<Node>();

            //Node no = new Node();
            //no.nome = "1-Burglary";
            //no.pais = new List<Node>();
            //no.tabela = new string[1, 1] { { "0,001" } };

            //nos.Add(no);

            //Node no2 = new Node();
            //no2.nome = "2-Earthquake";
            //no2.pais = new List<Node>();
            //no2.tabela = new string[1, 1] { { "0,002" } };

            //nos.Add(no2);


            //Node no3 = new Node();
            //no3.nome = "3-Alarm";
            //no3.pais = new List<Node>();
            //no.pais.Add(no);
            //no.pais.Add(no2);
            //no3.tabela = new string[4, 3] {{"true","true","0,95"},
            //                              {"true","false","0,94"},
            //                              {"false","true","0,29"},
            //                              {"false","false","0,001"} };


            //Node no4 = new Node();
            //no4.nome = "4-JohnCalls";
            //no4.pais = new List<Node>();
            //no4.pais.Add(no3);
            //no4.tabela = new string[2, 2] { { "true",  "0,90" },
            //                                { "false" ,"0,05"}};


            //Node no5 = new Node();
            //no5.nome = "5-MaryCalls";
            //no5.pais = new List<Node>();
            //no5.pais.Add(no3);
            //no5.tabela = new string[2, 2] { { "true",  "0,70" },
            //                                { "false" ,"0,01"}};



            Node no = new Node();
            no.nome = "B";
            no.pais = new List<Node>();
            no.tabela = new string[1, 1] { { "0,9" } };

            nos.Add(no);

            Node no3 = new Node();
            no3.nome = "M";
            no3.pais = new List<Node>();
            no3.tabela = new string[1, 1] { { "0,1" } };

            nos.Add(no3);


            Node no2 = new Node();
            no2.nome = "I";
            no2.pais = new List<Node>();
            no2.pais.Add(no);
            no2.pais.Add(no3);
            no2.tabela = new string[4, 3] {{"true","true","0,9"},
                                          {"true","false","0,5"},
                                          {"false","true","0,5"},
                                          {"false","false","0,1"} };


            nos.Add(no2);



            Node no4 = new Node();
            no4.nome = "G";
            no4.pais = new List<Node>();
            no4.pais.Add(no3);
            no4.pais.Add(no);
            no4.pais.Add(no2);
            no4.tabela = new string[8, 4] {{"true","true","true", "0,9"},
                                          {"true","true","false", "0,8"},
                                          {"true","false","true", "0"},
                                          {"true","false","false", "0"},
                                          {"false","true","true", "0,2"},
                                          {"false","true","false", "0,1"},
                                          {"false","false","true", "0"},
                                          {"false","false","false", "0"}};

            nos.Add(no4);

            Node no5 = new Node();
            no5.nome = "J";
            no5.pais = new List<Node>();
            no5.pais.Add(no3);
            no5.tabela = new string[2, 2] { { "true",  "0,9" },
                                            { "false" ,"0,0"}};

            nos.Add(no5);



            //Calcule o valor de P(b, i, ¬m, g, j).


            //calcular P(b, i, m)

            //b, i e m são evidências, então são colocados como true


            //Alterar aqui o número de amostras desejadas

            for (int z = 0; z < 10; z++)
            {




                for (int i = 0; i < nos.Count; i++)
                {

                    if (nos[i].nome.Contains("B") || nos[i].nome.Contains("I") || nos[i].nome.Contains("M"))
                    {

                        nos[i].valor = true;

                    }
                    else
                    {
                        nos[i].valor = null;
                    }

                }







                //inicializando W e X

                List<string> W = new List<string>();
                List<string> X = new List<string>();

                W.Add("1");


                for (int j = 0; j < nos.Count; j++)
                {

                    //se não tem pais, verifica se foi dado como evidência (se já possui um valor true/false). Se for true, pega o valor do nó, Caso contrário, (1-o valor)

                    if (nos[j].pais.Count == 0)
                    {

                        //caso o nó não possua nem true nem false, será feito o sampling (gerar número aleatório e verificar a probabilidade para de acordo com esta, gerar true ou false para aquele nó)
                        if (nos[j].valor == null)
                        {
                            double rnd = GetRandomNumber(0, 1);
                            if (Convert.ToDouble(nos[j].tabela[0, 0]) <= rnd)
                            {
                                nos[j].valor = true;
                            }
                            else
                            {
                                nos[j].valor = false;
                            }
                        }

                        if (nos[j].valor == true)
                        {
                            X.Add(nos[j].nome);
                            W.Add(nos[j].tabela[0, 0]);
                        }
                        else
                        {
                            X.Add("¬" + nos[j].nome);
                            W.Add((1 - Convert.ToDouble(nos[j].tabela[0, 0])).ToString());
                        }

                    }

                    //se tiver pais, rodar um for (na verdade 2 aninhados) na tabela olhando até o n-ésimo valor, onde n é o número de pais
                    //O número de pais do nó determinará os cálculos que devem ser feitos antes de poder fazer o cálculo do nó atual e saber quantas colunas da tabela deverão ser lidas (ex. linha: true true false 0.9, representam 3 colunas de nós pais. A última é o valor a ser pegue )


                    if (nos[j].pais.Count > 0)
                    {


                        string busca = "";
                        for (int k = 0; k < nos[j].pais.Count; k++)
                        {
                            busca = busca + nos[j].pais[k].valor;
                        }


                        //fazer um somatorio de linha da matriz e verificar se contém a busca

                        var tabela = nos[j].tabela;
                        string result = "";

                        var rowCount = tabela.GetLength(0);
                        var colCount = tabela.GetLength(1);
                        for (int row = 0; row < rowCount; row++)
                        {
                            string linha = "";
                            for (int col = 0; col < colCount; col++)
                            {

                                linha = linha + tabela[row, col];

                            }


                            if (linha.Contains(busca.ToLower()))
                            {
                                result = linha.Replace(busca.ToLower(), "");
                            }


                        }


                        //verificando se é evidência
                        if (nos[j].valor == null)
                        {
                            var random = GetRandomNumber(0, 1);
                            var resultado = Convert.ToDouble(result);

                            if (random < resultado)
                            {

                                nos[j].valor = true;

                                X.Add(nos[j].nome);
                                W.Add(result);


                            }
                            else
                            {

                                nos[j].valor = false;

                                X.Add("¬" + nos[j].nome);
                                W.Add((1 - Convert.ToDouble(result)).ToString());

                            }


                        }
                        else
                        {
                            //caso já seja evidência não há por que fazer sample

                            if (nos[j].valor == true)
                            {
                                X.Add(nos[j].nome);
                                W.Add(result);
                            }
                            else
                            {
                                X.Add("¬" + nos[j].nome);
                                W.Add((1 - Convert.ToDouble(result)).ToString());
                            }

                        }




                    }


                }

                Console.WriteLine();
                Console.WriteLine("Valores de X:");
                for (int p = 0; p < X.Count; p++)
                {

                    Console.Write(X[p] + "; ");

                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Valores de W:");
                for (int p = 0; p < W.Count; p++)
                {

                    Console.Write(W[p] + "; ");

                }

                Console.WriteLine();
                Console.WriteLine("Total do produtório:");
                Console.WriteLine(Produtorio(W));

                Console.WriteLine("--------");

            }


        }

        public static double Produtorio(List<string> W)
        {
            double prod = 1;

            for (int i = 0; i < W.Count; i++)
            {
                prod = prod * Convert.ToDouble(W[i]);
            }


            return prod;

        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
