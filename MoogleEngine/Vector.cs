// Ignore Spelling: IDF

namespace MoogleEngine;

public class Vector
    {
        //Intstance Variables
        private double[] components;
        private int dimension;
        private double module;
        //Properties
        private double Module
        {
            get { return module; }
        }
        //Methods
        public Vector(double[] components) //Constructor
        {
            this.components = components;
            this.dimension = this.components.Length;
            this.module = GetModule();
        }
        private double GetModule()
        {
            double Sum = 0;
            foreach (var component in this.components)
            {
                Sum += (float)Math.Pow(component, 2);
            }
            return Math.Sqrt(Sum);
        }
        private static double DotProduct(Vector v1, Vector v2)
        {
            double res = 0;
            for (int i = 0; i < v1.dimension; i++)
            {
                res += v1.components[i] * v2.components[i];
            }
            return res;
        }
        public static double Cos(Vector v1, Vector v2)
        {
            if (v1.Module != 0 && v2.Module != 0)
            {
                return DotProduct(v1, v2) / (v1.Module * v2.Module);
            }
            else
            {
                return 0;
            }
        }

    }

