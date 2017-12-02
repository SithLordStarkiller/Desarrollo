using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class TreeNode
    {
        public string text { get; set; }
        public bool leaf { get; set; }
        public List<TreeNode> children { get; set; }
        public string iconCls { get; set; }
        public int idMenu { get; set; }
        public Boolean pmCaptura { get; set; }
        public Boolean pmModifica { get; set; }
        public Boolean expanded { get; set; }
        public TreeNode()
        {
            idMenu = 0;
            leaf = false;
            children = new List<TreeNode>();
        }


    }
}
