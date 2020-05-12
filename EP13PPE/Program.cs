using System;
using System.Collections.Generic;

namespace EP13PPE
{
    class Program
    {
        static void Main(string[] args)
        {

            //Criando e atribuindo valores aos nós da rede

            List<Node> nos = new List<Node>();

            Node no = new Node();
            no.nome = "1-Burglary";
            no.pais = new List<Node>();
            no.tabela = new string[1, 1] { { "0,001" } };

            nos.Add(no);

            Node no2 = new Node();
            no2.nome = "2-Earthquake";
            no2.pais = new List<Node>();
            no2.tabela = new string[1, 1] { { "0,002" } };

            nos.Add(no2);


            Node no3 = new Node();
            no3.nome = "3-Alarm";
            no3.pais = new List<Node>();
            no.pais.Add(no);
            no.pais.Add(no2);
            no3.tabela = new string[4, 3] {{"true","true","0,95"},
                                          {"true","false","0,94"},
                                          {"false","true","0,29"},
                                          {"false","false","0,001"} };


            Node no4 = new Node();
            no4.nome = "4-JohnCalls";
            no4.pais = new List<Node>();
            no4.pais.Add(no3);
            no4.tabela = new string[2, 2] { { "true",  "0,90" },
                                            { "false" ,"0,05"}};


            Node no5 = new Node();
            no5.nome = "5-MaryCalls";
            no5.pais = new List<Node>();
            no5.pais.Add(no3);
            no5.tabela = new string[2, 2] { { "true",  "0,70" },
                                            { "false" ,"0,01"}};


        }
    }
}
