using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLPIP3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool finished = false;
            do 
            {
                StreamReader input;
                try
                {
                    Console.Write("Enter a file: ");
                    input = new StreamReader(Console.ReadLine());
                    finished = true;
                }
                catch (FileNotFoundException)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: File not found!");
                    Console.ForegroundColor = originalColor;
                    continue;
                }
                while (!input.EndOfStream)
                {
                    //try
                    //{
                        string line = input.ReadLine();
                        TreeNode head = createExpressionTree(line);
                        Console.WriteLine(convertToPrefix(head));
                    //}
                    //catch (Exception)
                    //{
                    //    Console.WriteLine("Rats! Something went wrong. Moving on...");
                    //}
                }
            } while (!finished);
        }

        private static TreeNode createExpressionTree(string infixExpression)
        {
            Stack<TreeNode> subTreeStack = new Stack<TreeNode>();
            Stack<char> firstPriorityOperators = new Stack<char>();
            Stack<char> secondPriorityOperators = new Stack<char>();
            Stack<int> leftParenthesesStack = new Stack<int>();
            for (int i = 0; i < infixExpression.Length; i++)
            {
                if (infixExpression[i] == '(')
                {
                    leftParenthesesStack.Push(i);
                }
                else if (infixExpression[i] == ')')
                {
                    int firstIndex = leftParenthesesStack.Pop();
                    if (leftParenthesesStack.Count == 0)
                    {
                        TreeNode node = createExpressionTree(infixExpression.Substring(firstIndex + 1, i - firstIndex - 1));
                        if (firstPriorityOperators.Count > 0)
                        {
                            subTreeStack.Push(new TreeNode(firstPriorityOperators.Pop(), subTreeStack.Pop(), node));
                        }
                        else
                            subTreeStack.Push(node); 
                    }
                }
                else if (leftParenthesesStack.Count == 0)
                {
                    if (infixExpression[i] == '+' || infixExpression[i] == '-')
                    {
                        secondPriorityOperators.Push(infixExpression[i]);
                    }
                    else if (infixExpression[i] == '*' || infixExpression[i] == '+')
                    {
                        firstPriorityOperators.Push(infixExpression[i]);
                    }
                    else
                    {
                        TreeNode node = new TreeNode(infixExpression[i], null, null);
                        if (firstPriorityOperators.Count > 0)
                        {
                            subTreeStack.Push(new TreeNode(firstPriorityOperators.Pop(), subTreeStack.Pop(), node));
                        }
                        else 
                            subTreeStack.Push(node);
                    }
                }
            }
            while (secondPriorityOperators.Count > 0)
            {
                TreeNode right = subTreeStack.Pop();
                subTreeStack.Push(new TreeNode(secondPriorityOperators.Pop(), subTreeStack.Pop(), right));
            }
            return subTreeStack.Pop();
        }

        private static string convertToPrefix(TreeNode root)
        {
            if (root == null) return "";
            else return root.getValue() + convertToPrefix(root.getLeft()) + convertToPrefix(root.getRight());
        }
    }
}
