using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm3
    {
        private Panel mainPanel;
        private RichTextBox newGroupList;
        private DrawingForm3 df3;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][] distinctGroups;

        public ControlsForm3(Panel panel, RichTextBox newGroupList)
        {
            mainPanel = panel;
            this.newGroupList = newGroupList;
        }

        public void ClearAndStart(int[][] groups, string[][] mainArray)
        {
            Calculation3 c3 = new Calculation3(groups.Clone() as int[][], mainArray.Clone() as string[][]);
            c3.StartCalculation(out relationMatrix, out distinctGroups);

            df3 = new DrawingForm3();
            df3.StartDraw(mainPanel, relationMatrix, distinctGroups);

            newGroupList.Text = "";

        }

        public void ChangeState()
        {
            df3.ChangeFormState(mainPanel);
            mainPanel = null;
            newGroupList = null;
            df3 = null;
        }
    }
}
