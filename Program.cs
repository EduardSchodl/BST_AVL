/**
 * Vrchol binarniho vyhledavaciho stromu (BST) se jmeny souboru
 */
public class Node
{
    /** Klic - jmeno souboru */
    public String key;
    /** Levy potomek */
    public Node left;
    /** Pravy potomek */
    public Node right;

    public Node parent;

    public int height;
    public int balance;

    /**
    * Vytvori novy vrchol binarniho vyhledavaciho stromu
    */
    public Node(String key, Node parent)
    {
        this.key = key;
        this.parent = parent;
    }
}

/**
 * Binarni vyhledavaci strom se jmeny souboru
 */
class BinarySearchTree
{
    /** Koren binarniho vyhledavaciho stromu */
    public Node root;

    /**
    * Prida do BST prvek se zadanym klicem - jmenem souboru  
    */
    public void Add(String key)
    {
        if (root == null)
        {
            root = new Node(key, null);
        }
        else
        {
            AddUnder(root, key);
        }
    }

    private static bool Smaller(string s, string s2)
    {
        String s1sm = s.ToLower();
        String s2sm = s2.ToLower();
        return s1sm.CompareTo(s2sm) < 0;
    }

    /**
    * Vlozi pod zadany vrchol novy vrchol se zadanym klicem
    */
    void AddUnder(Node n, String key)
    {
        if (Smaller(key, n.key))
        {
            // uzel patri doleva, je tam misto?
            if (n.left == null)
            {
                Node newNode = new Node(key, n);
                n.left = newNode;
            }
            else
            {
                AddUnder(n.left, key);
            }
        }
        else
        {
            // uzel patri doprava, je tam misto?
            if (n.right == null)
            {
                Node newNode = new Node(key, n);
                n.right = newNode;
            }
            else
            {
                AddUnder(n.right, key);
            }
        }

        UpdateNode(n);
        UpdateNode(n.right);
        UpdateNode(n.left);
        
        if(n.balance == -2 && n.left.balance == 1)
        {
            //Console.WriteLine("LR");
            RotateLeft(n.left);
            RotateRight(n);
        }

        if (n.balance == 2 && n.right.balance == -1)
        {
            //Console.WriteLine("RL");
            RotateRight(n.right);
            RotateLeft(n);
        }

        if (n.balance < -1)
        {
            //Console.WriteLine("right");
            RotateRight(n);
        }

        if (n.balance > 1)
        {
            //Console.WriteLine("left");
            RotateLeft(n);
        }
        
    }


    //Metoda Contains
    public bool Contains(string key)
    {
        Node current = root;

        if (current == null)
        {
            Console.WriteLine("Strom neobsahuje daná řezězec");
            return false;
        }
        while (current != null)
        {
            if (key.CompareTo(current.key) < 0)
            {
                current = current.left;
            }
            else if (key.CompareTo(current.key) > 0)
            {
                current = current.right;
            }
            else if (key.CompareTo(current.key) == 0)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Metoda aktualizuje výšku a vyvážení vrcholu <b>n</b>.
    /// </summary>
    /// <param name="n">Aktualizovaný vektor</param>
    public void UpdateNode(Node n)
    {
        if(n == null)
        {
            return;
        }
        int leftHeight = n.left != null ? n.left.height : -1;
        int rightHeight = n.right!= null ? n.right.height : -1;

        if (leftHeight == -1 && rightHeight == -1)
        {
            n.height = 0;
        }
        else
        {
            n.height = 1 + Math.Max(rightHeight, leftHeight);
        }
        
        n.balance = rightHeight - leftHeight;
    }

    /// <summary>
    /// Metoda provede pravou rotaci přes vrchol <b>n</b>.
    /// </summary>
    /// <param name="n">Vrchol, přes který se provádí rotace</param>
    public void RotateRight(Node n)
    {
        Node y = n;
        Node x = n.left;
        Node a = n.left.left;
        Node b = n.left.right;
        Node c = n.right;
        Node p = y.parent;

        x.right = y;
        y.parent = x;

        y.left = b;
        if (b != null)
            b.parent = y;

        x.parent = p;
        if (p != null)
        {
            if (p.left == y)
                p.left = x;
            else
                p.right = x;
        }
        else
            root = x;

        UpdateNode(y);
        UpdateNode(x);
    }

    /// <summary>
    /// Metoda provede levou rotaci přes vrchol <b>n</b>.
    /// </summary>
    /// <param name="n">Vrchol, přes který se provádí rotace</param>
    public void RotateLeft(Node n)
    {
        Node x = n;
        Node y = n.right;
        Node a = n.left;
        Node b = n.right.left;
        Node c = n.right.right;
        Node p = x.parent;

        y.left = x;
        x.parent = y;

        x.right = b;
        if (b != null)
            b.parent = x;

        y.parent = p;
        if (p != null)
        {
            if (p.left == x)
                p.left = y;
            else
                p.right = y;
        }
        else
            root = y;

        UpdateNode(x);
        UpdateNode(y);
    }

    /// <summary>
    /// Metoda ověří splnění podmínky AVL stromu.
    /// </summary>
    /// <param name="n">Vrchol, od které se začíná s kontrolou (root)</param>
    /// <returns>True, pokud splňuje, jinak false</returns>
    public bool isAVL(Node n)
    {
        if (n != null)
        {
            
            if (n.balance < -1 || n.balance > 1)
            {
                return false;
            }
            
            isAVL(n.left);
            Console.WriteLine(n.key + ": " + n.balance);
            isAVL(n.right);
        }
        return true;
    }
}


/**
    * Trida pro doplnovani textu na zaklade historie
    */
public class Autocomplete
{

    public static void Main(String[] args)
    {
        /*
        BinarySearchTree bst = new BinarySearchTree();
        //bst.Add("http://portal.zcu.cz");
        //bst.Add("http://courseware.zcu.cz");
        //bst.Add("http://pfortal.zcu.cz");
        //bst.Add("http://pgaourseware.zcu.cz");

        bst.Add("http://port");
        bst.Add("http://cour");
        bst.Add("http://czur");


        //        Console.WriteLine(bst.Contains("http://portal.zcu.cz"));
        //      Console.WriteLine(bst.Contains("http://porl.zcu.cz"));

        Console.WriteLine(bst.isAVL(bst.root));
        */

        BinarySearchTree bst = new BinarySearchTree();
        StreamReader sr = new StreamReader("requests.txt");

        string line = "";
        while ((line = sr.ReadLine()) != null)
        {
            String[] lineArr = line.Split(" ");
            switch (lineArr[0].Trim())
            {
                case "A":
                    bst.Add(lineArr[1].Trim());
                    break;
            }
        }

        Console.WriteLine($"Je AVL strom? - {bst.isAVL(bst.root)}");

        Console.WriteLine($"Výška kořene: {bst.root.height}         Vyvážení kořene: {bst.root.balance}");
    }
}