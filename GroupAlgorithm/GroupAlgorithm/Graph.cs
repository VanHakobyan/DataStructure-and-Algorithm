using System;
using System.IO;

namespace GroupAlgorithm
{
    // Used to signal violations of preconditions for
    // various shortest path algorithms.
   // Represents an edge in the graph.
    class Edge
    {
        public Vertex dest; // Second vertex in Edge
        public double cost; // Edge cost

        public Edge(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }
    }

// Represents an entry in the priority queue for Dijkstra's algorithm.
    class Path : System.Collections.Generic.IComparable<Path>
    {
        public Vertex dest; // w
        public double cost; // d(w)

        public Path(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }

        public int CompareTo(Path rhs)
        {
            double otherCost = rhs.cost;

            return cost < otherCost ? -1 : cost > otherCost ? 1 : 0;
        }

        public bool Equals(Path rhs)
        {
            double otherCost = rhs.cost;

            return cost == otherCost;
        }
    }

// Represents a vertex in the graph.
    class Vertex
    {
        public string name; // Vertex name
        public IList<Edge> adj; // Adjacent vertices
        public double dist; // Cost
        public Vertex prev; // Previous vertex on shortest path
        public int scratch; // Extra variable used in algorithm

        public Vertex(string nm)
        {
            name = nm;
            adj = new List<Edge>();
            reset();
        }

        public void reset()
        {
            dist = Graph.INFINITY;
            prev = null;
            pos = null;
            scratch = 0;
        }

        public IPriorityQueuePosition<Path> pos; // Used for dijkstra2 (Chapter 23)
    }

// Graph class: evaluate shortest paths.
//
// CONSTRUCTION: with no parameters.
//
// ******************PUBLIC OPERATIONS**********************
// void AddEdge( string v, string w, double cvw )
//                              --> Add additional edge
// void PrintPath( string w )   --> Print path after alg is run
// void Unweighted( string s )  --> Single-source unweighted
// void Dijkstra( string s )    --> Single-source weighted
// void Negative( string s )    --> Single-source negative weighted
// void Acyclic( string s )     --> Single-source acyclic
// ******************ERRORS*********************************
// Some error checking is performed to make sure graph is ok,
// and to make sure graph satisfies properties needed by each
// algorithm.  Exceptions are thrown if errors are detected.

    public class Graph
    {
        public const double INFINITY = double.MaxValue;
        private IDictionary<string, Vertex> vertexMap = new Dictionary<string, Vertex>();
        private int numVertices = 0;

        // Add a new edge to the graph.
        public void AddEdge(string sourceName, string destName, double cost)
        {
            Vertex v = GetVertex(sourceName);
            Vertex w = GetVertex(destName);
            v.adj.Add(new Edge(w, cost));
        }

        // Driver routine to handle unreachables and print total cost.
        // It calls recursive routine to print shortest path to
        // destNode after a shortest path algorithm has run.
        public void PrintPath(string destName)
        {
            Vertex w = vertexMap[destName]; // throws an exception if not found
            if (w.dist == INFINITY)
                Console.WriteLine(destName + " is unreachable");
            else
            {
                Console.Write("(Cost is: " + w.dist + ") ");
                PrintPath(w);
                Console.WriteLine();
            }
        }

        // If vertexName is not present, add it to vertexMap.
        // In either case, return the Vertex.
        private Vertex GetVertex(String vertexName)
        {
            if (vertexMap.ContainsKey(vertexName))
                return vertexMap[vertexName];
            else
            {
                Vertex v = new Vertex(vertexName);
                vertexMap[vertexName] = v;
                numVertices++;
                return v;
            }
        }

        // Recursive routine to print shortest path to dest
        // after running shortest path algorithm. The path
        // is known to exist.
        private void PrintPath(Vertex dest)
        {
            if (dest.prev != null)
            {
                PrintPath(dest.prev);
                Console.Write(" to ");
            }
            Console.Write(dest.name);
        }

        // Initializes the vertex output info prior to running
        // any shortest path algorithm.
        private void ClearAll()
        {
            foreach (Vertex v in vertexMap.Values)
                v.reset();
        }

        // Single-source unweighted shortest-path algorithm.
        public void Unweighted(string startName)
        {
            ClearAll();

            Vertex start = vertexMap[startName]; // throws an exception if not found

            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(start);
            start.dist = 0;

            while (q.Count > 0)
            {
                Vertex v = q.Dequeue();

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    if (w.dist == INFINITY)
                    {
                        w.dist = v.dist + 1;
                        w.prev = v;
                        q.Enqueue(w);
                    }
                }
            }
        }

        // Single-source weighted shortest-path algorithm.
        public void Dijkstra(string startName)
        {
            IPriorityQueue<Path> pq = new BinaryHeap<Path>();

            Vertex start = vertexMap[startName]; // throws an exception if not found

            ClearAll();
            pq.Insert(new Path(start, 0));
            start.dist = 0;

            int nodesSeen = 0;
            while (!pq.IsEmpty() && nodesSeen < numVertices)
            {
                Path vrec = pq.DeleteMin();
                Vertex v = vrec.dest;
                if (v.scratch != 0) // already processed v
                    continue;

                v.scratch = 1;
                nodesSeen++;

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    double cvw = e.cost;

                    if (cvw < 0)
                        throw new GraphException("Graph has negative edges");

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                        pq.Insert(new Path(w, w.dist));
                    }
                }
            }
        }

        // Single-source weighted shortest-path algorithm using pairing heaps.
        public void Dijkstra2(string startName)
        {
            IPriorityQueue<Path> pq = new PairingHeap<Path>();

            Vertex start = vertexMap[startName]; // throws an exception if not found

            ClearAll();
            start.pos = pq.Insert(new Path(start, 0));
            start.dist = 0;

            while (!pq.IsEmpty())
            {
                Path vrec = pq.DeleteMin();
                Vertex v = vrec.dest;

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    double cvw = e.cost;

                    if (cvw < 0)
                        throw new GraphException("Graph has negative edges");

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;

                        Path newVal = new Path(w, w.dist);
                        if (w.pos == null)
                            w.pos = pq.Insert(newVal);
                        else
                            pq.DecreaseKey(w.pos, newVal);
                    }
                }
            }
        }

        // Single-source negative-weighted shortest-path algorithm.
        public void Negative(string startName)
        {
            ClearAll();

            Vertex start = vertexMap[startName]; // throws an exception if not found

            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(start);
            start.dist = 0;
            start.scratch++;

            while (q.Count > 0)
            {
                Vertex v = q.Dequeue();
                if (v.scratch++ > 2 * numVertices)
                    throw new GraphException("Negative cycle detected");

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    double cvw = e.cost;

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                        // Enqueue only if not already on the queue
                        if (w.scratch++ % 2 == 0)
                            q.Enqueue(w);
                        else
                            w.scratch--; // undo the enqueue increment    
                    }
                }
            }
        }

        // Single-source negative-weighted acyclic-graph shortest-path algorithm.
        public void Acyclic(string startName)
        {
            Vertex start = vertexMap[startName]; // throws an exception if not found

            ClearAll();
            Queue<Vertex> q = new Queue<Vertex>();
            start.dist = 0;

            // Compute the indegrees
            ICollection<Vertex> vertexSet = vertexMap.Values;
            foreach (Vertex v in vertexSet)
            foreach (Edge e in v.adj)
                e.dest.scratch++;

            // Enqueue vertices of indegree zero
            foreach (Vertex v in vertexSet)
                if (v.scratch == 0)
                    q.Enqueue(v);

            int iterations;
            for (iterations = 0; q.Count > 0; iterations++)
            {
                Vertex v = q.Dequeue();

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    double cvw = e.cost;

                    if (--w.scratch == 0)
                        q.Enqueue(w);

                    if (v.dist == INFINITY)
                        continue;

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                    }
                }
            }

            if (iterations != numVertices)
                throw new GraphException("Graph has a cycle!");
        }

        // Process a request; return false if end of file.
        public static bool ProcessRequest(TextReader fin, Graph g)
        {
            string startName = null;
            string destName = null;
            string alg = null;

            try
            {
                Console.Write("Enter start node:");
                if ((startName = fin.ReadLine()) == null)
                    return false;
                Console.Write("Enter destination node:");
                if ((destName = fin.ReadLine()) == null)
                    return false;
                Console.Write(" Enter algorithm (u, d, n, a ): ");
                if ((alg = fin.ReadLine()) == null)
                    return false;

                if (alg == "u")
                    g.Unweighted(startName);
                else if (alg.Equals("d"))
                {
                    g.Dijkstra(startName);
                    g.PrintPath(destName);
                    g.Dijkstra2(startName);
                }
                else if (alg == "n")
                    g.Negative(startName);
                else if (alg == "a")
                    g.Acyclic(startName);

                g.PrintPath(destName);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e);
            }
            catch (GraphException e)
            {
                Console.Error.WriteLine(e);
            }
            return true;
        }

        // A main routine that:
        // 1. Reads a file containing edges (supplied as a command-line parameter);
        // 2. Forms the graph;
        // 3. Repeatedly prompts for two vertices and
        //    runs the shortest path algorithm.
        // The data file is a sequence of lines of the format
        //    source destination.
        public static void Main(string[] args)
        {
            Graph g = new Graph();
            try
            {
                //     TextReader fin = new StreamReader( args[0] );
                TextReader fin = new StreamReader("c:\\c#\\code\\graph2.txt");

                // Read the edges and insert
                string line;
                while ((line = fin.ReadLine()) != null)
                {
                    string[] st = line.Split();

                    try
                    {
                        if (st.Length != 3)
                        {
                            Console.Error.WriteLine("Skipping ill-formatted line " + line);
                            continue;
                        }
                        string source = st[0];
                        string dest = st[1];
                        int cost = int.Parse(st[2]);
                        g.AddEdge(source, dest, cost);
                    }
                    catch (FormatException)
                    {
                        Console.Error.WriteLine("Skipping ill-formatted line " + line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e);
            }

            Console.Error.WriteLine("File read...");
            Console.Error.WriteLine(g.numVertices + " vertices");

            while (ProcessRequest(Console.In, g))
                ;
        }
    }
}