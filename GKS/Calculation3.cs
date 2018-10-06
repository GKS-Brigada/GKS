using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation3
    {
        private int[][] group;
        private string[][] distinctGroups;
        private string[][] mainArray;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;

        public Calculation3(int[][] group, string[][] mainArray)
        {
            this.group = group;
            this.mainArray = mainArray;
        }

        public void StartCalculation()
        {
            JoinGroup();
            //GroupRecombination();

            /*distinctGroups = this.distinctGroups;
            newGroups = group;
            groupPosition = this.groupPosition;*/
        }

        private void JoinGroup()
        {
            string[] selectGroup = new string[group.Length];
            for (int i = 0; i < group.Length; i++)
            {
                for (int j = 0; j < group[i].Length; j++)
                {
                    for (int k = 0; k < mainArray[group[i][j] - 1].Length; k++)
                    {
                        selectGroup[i] += mainArray[group[i][j] - 1][k];
                        selectGroup[i] += " ";
                    }
                }
            }
            distinctGroups = new string[group.Length][];
            for (int i = 0; i < group.Length; i++)
            {
                distinctGroups[i] = selectGroup[i].Split().Distinct().ToArray();
            }
        }

        private void FormRelationMatrix()
        {
            relationMatrix = new Dictionary<string, Dictionary<string, int>>[group.Length];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                relationMatrix[i] = new Dictionary<string, Dictionary<string, int>>();
                for(int j = 0; j < distinctGroups[i].Length; j++)
                {
                    Dictionary<string, int> temp = new Dictionary<string, int>();
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        temp.Add(distinctGroups[i][k], 0);
                    }
                    relationMatrix[i].Add(distinctGroups[i][j], temp);
                }
            }

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (j != 0 && distinctGroups[i][j - 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = 1;
                        }
                        else if (j != distinctGroups[i].Length - 1 && distinctGroups[i][j + 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = -1;
                        }
                    }
                }
            }
        }

        //-----???????????

        /*private int[][][] FirstOperation()
        {
            int[][][] firstRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                firstRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    firstRelation[i][j] = new int[distinctGroups[i].Length];
                    bool first = true;
                    
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (j != distinctGroups[i].Length - 1 && distinctGroups[i][j + 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = -1;
                        }
                        else if (j != 0 && distinctGroups[i][j - 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = 1;
                        }
                    }
                }
            }
        }*/

        private int[][][] SecondOperation()
        {
            int[][][] secondRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                secondRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    secondRelation[i][j] = new int[distinctGroups[i].Length];
                    bool second = true;

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (j != k && relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] != 1)
                            second = false;
                    }

                    if(second)
                    {
                        for (int k = 0; k < distinctGroups[i].Length; k++)
                        {
                            secondRelation[i][j][k] = 1;
                        }
                    }
                }
            }

            return secondRelation;
        }

        private int[][][] ThirdOperation()
        {
            int[][][] thirdRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                thirdRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    thirdRelation[i][j] = new int[distinctGroups[i].Length];

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 1)
                            if(relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1)
                            {
                                thirdRelation[i][j][k] = 1;
                                thirdRelation[i][k][j] = 1;
                            }
                    }
                }
            }
            //kek
            return thirdRelation;
        }
    }
}
