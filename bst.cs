using System.Text;

namespace BinarySearchTree;

public class BinarySearchTree
{
    public Node? root;

    public void Add(int value)
    {
        if (root == null)
        {
            root = new Node(value);
        }
        else
        {
            root.Add(value);
        }
    }

    public Node Balance()
    {
        List<Node> nodes = [];
        Store(root!, nodes);
        return BuildTree(nodes, 0, nodes.Count - 1)!;
    }
    public static void PreOrder(Node node)
    {
        if (node == null)
        {
            return;
        }
        Console.WriteLine("{0}", node.Value);
        PreOrder(node.Left!);
        PreOrder(node.Right!);
    }

    public Node? Search(int val)
    {
        return SearchHelper(root!, val);
    }

    private static Node? SearchHelper(Node? node, int val)
    {
        if (node == null)
        {
            return null;
        }
        switch (node.Value.CompareTo(val))
        {
            case 0: return node;
            case < 0: return SearchHelper(node.Right, val);
            case > 0: return SearchHelper(node.Left, val);
        }
    }

    private static Node? BuildTree(List<Node> nodes, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        int mid = (start + end) / 2;
        Node node = nodes[mid];

        node.Left = BuildTree(nodes, start, mid - 1);
        node.Right = BuildTree(nodes, mid + 1, end);
        return node;
    }

    private static void Store(Node root, List<Node> nodes)
    {
        if (root == null)
        {
            return;
        }
        Store(root.Left!, nodes);
        nodes.Add(root);
        Store(root.Right!, nodes);
    }
}


public class Node
{
    public int Value;
    public Node? Left, Right;

    public Node(int data)
    {
        Value = data;
        Left = Right = null;
    }

    public void Add(int newValue)
    {
        if (newValue < Value)
        {
            if (Left == null)
            {
                Left = new(newValue);
            }
            else
            {
                Left.Add(newValue);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = new(newValue);
            }
            else
            {
                Right.Add(newValue);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Node: \n");
        sb.AppendFormat("\tValue: {0}\n", Value);
        sb.AppendFormat("\tLeft: {0}\n", Left?.ToString() ?? "null");
        sb.AppendFormat("\tRight: {0}", Right?.ToString() ?? "null");

        return sb.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var tree = new BinarySearchTree();

        foreach (var val in new[] { 8, 5, 6, 4, 1, 2 })
        {
            tree.Add(val);
        }

        BinarySearchTree.PreOrder(tree.root!);
        tree.root = tree.Balance();
        Console.WriteLine("New Preorder: ");
        BinarySearchTree.PreOrder(tree.root!);

        var searchableValue = tree.Search(5);
        Console.WriteLine("Found: \n{0}", searchableValue!.ToString());
        var unsearchableValue = tree.Search(10);
        Console.WriteLine("Found: {0}", unsearchableValue?.ToString());
    }
}