using System;
using System.Collections.Generic;
using System.Text;

namespace EP13PPE
{
    public class Node
    {

        public string nome { get; set; }
        public List<Node> pais { get; set; }
        public bool? valor { get; set; }
        public string[,] tabela { get; set; }

    }
}
