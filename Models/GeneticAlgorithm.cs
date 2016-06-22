/*The route Optimising SideKick was created as a 4th year final project fior Gavin Gaughran x12107077 National College of Ireland 16.05.16

Code used throughout was written by Gavin Gaughran using, references and modified snippets from to the following websites:
    StackOverflow(General coding practice and troubleshooting)
    Code.MSDN(Algorithm mathematical equation)
    Damien Dennehy(Haversine information)
    Rubicite(PMX Crossover information)
    JSFiddle(JQuery and JavaScript functionality)
    W3Schools(CSS and design)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RouteOp
{
    class GeneticAlgorithm
    {
        //Declaring variables
        private double minimumDistance;
        private double crossoverRate;
        private double[] lng, lat;

        private int g;
        private int generations;
        private int numOfDestinations;
        private int population;
        private int[] minimumDistanceResult;

        private Random random;

        public double MinimumDistance
        {
            get { return minimumDistance; }
        }

        public int G
        {
            get { return g; }
        }

        public int[] MinimumDistanceResult
        {
            get { return minimumDistanceResult; }
        }

        public int[] changeOrder(int startPoint)
        {
            if (startPoint == 0)
            {
                return minimumDistanceResult;
            }

            int[] myChangedOrder = new int[minimumDistanceResult.Length];

            var StartObject = minimumDistanceResult[startPoint];
            int start = startPoint;
            int i = 0, x = 0;

            myChangedOrder[i++] = StartObject;
            
            //copy the last n elemnts
            while (++startPoint < minimumDistanceResult.Length)
            {
                myChangedOrder[i++] = minimumDistanceResult[startPoint];
            }

            //copy the fist N elements
            while (x < start && i < myChangedOrder.Length)
            {
                myChangedOrder[i] = minimumDistanceResult[x];
                i++;
                x++;
            }

            return myChangedOrder;
        }

        //Order the results before returning
        public void Order()
        {
            int i = 0;
            for (; i < minimumDistanceResult.Length; i++)
            {
                if (minimumDistanceResult[i] == 0)
                    break;
            }
            minimumDistanceResult = changeOrder(i);
        }

        public GeneticAlgorithm(double crossoverRate, double[] lng, double[] lat, int generations, int numOfDestinations, int population, int seed)
        {
            this.crossoverRate = crossoverRate;
            this.lng = lng;
            this.lat = lat;
            this.generations = generations;
            this.numOfDestinations = numOfDestinations;
            this.population = population;
            random = new Random(seed);
        }

        private double Distance(double long1, double lat1, double long2, double lat2)
        {
            double distanceNum=0;

            //isNaN() function determines whether a value is an illegal number (Not-a-Number).
            if (double.IsNaN(long1) || double.IsNaN(lat1) || double.IsNaN(long2) || double.IsNaN(lat2))
            {
               // throw new ArgumentException(SR.GetString("Argument_LatitudeOrLongitudeIsNotANumber"));
            }
            else
            {
                //Haversine Formula
                double latitude = long1 * 0.0174532925199433;
                double longitude = lat1 * 0.0174532925199433;
                double num = long2 * 0.0174532925199433;
                double longitude1 =lat2  * 0.0174532925199433;
                double num1 = longitude1 - longitude;
                double num2 = num - latitude;
                double num3 = Math.Pow(Math.Sin(num2 / 2), 2) + Math.Cos(latitude) * Math.Cos(num) * Math.Pow(Math.Sin(num1 / 2), 2);
                double num4 = 2 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1 - num3));
                distanceNum = 6376500 * num4;
              
            }
            return distanceNum;
        }

        // Partially Mapped Crossover Section (PMX)
        private void PartiallyMappedCrossover(int cutPoint1, int cutPoint2, int[] parentArray1, int[] parentArray2, int[] offSpring1, int[] offSpring2)
        {
            for (int i = 0; i < numOfDestinations; i++)
                offSpring1[i] = offSpring2[i] = -1;

            for (int i = cutPoint1; i <= cutPoint2; i++)
            {
                offSpring1[i] = parentArray2[i];
                offSpring2[i] = parentArray1[i];
            }

            for (int i = 0; i < cutPoint1; i++)
            {
                bool found = false;
                int t = parentArray1[i];

                for (int j = i + 1; !found && j < numOfDestinations; j++)
                    found = t == offSpring1[j];

                if (!found)
                    offSpring1[i] = t;
            }

            for (int i = cutPoint2 + 1; i < numOfDestinations; i++)
            {
                bool found = false;
                int t = parentArray1[i];

                for (int j = 0; !found && j < numOfDestinations; j++)
                    found = t == offSpring1[j];

                if (!found)
                    offSpring1[i] = t;
            }

            List<int> used = new List<int>();

            for (int i = 0; i < numOfDestinations; i++)
                if (offSpring1[i] != -1)
                    used.Add(offSpring1[i]);

            for (int i = 0; i < numOfDestinations; i++)
            {
                if (offSpring1[i] == -1)
                {
                    int lng;

                    do
                    {
                        lng = random.Next(numOfDestinations);
                    } while (used.Contains(lng));

                    offSpring1[i] = lng;
                    used.Add(lng);
                }
            }

            for (int i = 0; i < cutPoint1; i++)
            {
                bool found = false;
                int t = parentArray2[i];

                for (int j = i + 1; !found && j < numOfDestinations; j++)
                    found = t == offSpring2[j];

                if (!found)
                    offSpring2[i] = t;
            }

            for (int i = cutPoint2 + 1; i < numOfDestinations; i++)
            {
                bool found = false;
                int t = parentArray2[i];

                for (int j = 0; !found && j < numOfDestinations; j++)
                    found = t == offSpring2[j];

                if (!found)
                    offSpring2[i] = t;
            }

            used = new List<int>();

            for (int i = 0; i < numOfDestinations; i++)
                if (offSpring2[i] != -1)
                    used.Add(offSpring2[i]);

            for (int i = 0; i < numOfDestinations; i++)
            {
                if (offSpring2[i] == -1)
                {
                    int lng;

                    do
                    {
                        lng = random.Next(numOfDestinations);
                    } while (used.Contains(lng));

                    offSpring2[i] = lng;
                    used.Add(lng);
                }
            }
        }
        //MSDN Code
        private double TourDistance(int[] city)
        {
            double td = 0.0;

            for (int n = 0; n < numOfDestinations - 1; n++)
                td += Distance(lng[city[n]], lat[city[n]],
                    lng[city[n + 1]], lat[city[n + 1]]);

            td += Distance(lng[city[numOfDestinations - 1]], lat[city[numOfDestinations - 1]],
                lng[city[0]], lat[city[0]]);

            return td;
        }

        public void RunGenAlgo(BackgroundWorker backgroundWorkerThread)
        {
            double[] distance = new double[population];

            minimumDistanceResult = new int[numOfDestinations];

            int[,] chromosome = new int[population, numOfDestinations];

            minimumDistance = double.MaxValue;

            for (int p = 0; p < population; p++)
            {
                bool[] used = new bool[numOfDestinations];
                int[] city = new int[numOfDestinations];

                for (int n = 0; n < numOfDestinations; n++)
                    used[n] = false;

                for (int n = 0; n < numOfDestinations; n++)
                {
                    int i;

                    do
                    {
                        i = random.Next(numOfDestinations);
                    }
                    while (used[i]);

                    used[i] = true;
                    city[n] = i;
                }

                for (int n = 0; n < numOfDestinations; n++)
                    chromosome[p, n] = city[n];

                distance[p] = TourDistance(city);

                if (distance[p] < minimumDistance)
                {
                    minimumDistance = distance[p];

                    for (int n = 0; n < numOfDestinations; n++)
                        minimumDistanceResult[n] = chromosome[p, n];
                }
            }

            for (g = 0; g < generations; g++)
            {
                if (backgroundWorkerThread.CancellationPending)
                    return;

                if (random.NextDouble() < crossoverRate)
                {
                    int i, j, parent1, parent2;
                    int[] parentArray1 = new int[numOfDestinations];
                    int[] parentArray2 = new int[numOfDestinations];
                    int[] offSpring1 = new int[numOfDestinations];
                    int[] offSpring2 = new int[numOfDestinations];

                    i = random.Next(population);
                    j = random.Next(population);

                    if (distance[i] < distance[j])
                        parent1 = i;

                    else
                        parent1 = j;

                    i = random.Next(population);
                    j = random.Next(population);

                    if (distance[i] < distance[j])
                        parent2 = i;

                    else
                        parent2 = j;

                    for (i = 0; i < numOfDestinations; i++)
                    {
                        parentArray1[i] = chromosome[parent1, i];
                        parentArray2[i] = chromosome[parent2, i];
                    }

                    int cp1 = -1, cp2 = -1;

                    do
                    {
                        cp1 = random.Next(numOfDestinations);
                        cp2 = random.Next(numOfDestinations);
                    } while (cp1 == cp2 || cp1 > cp2);

                    PartiallyMappedCrossover(cp1, cp2, parentArray1, parentArray2, offSpring1, offSpring2);

                    double offSpring1Fitness = TourDistance(offSpring1);
                    double offSpring2Fitness = TourDistance(offSpring2);

                    if (offSpring1Fitness < distance[parent1])
                        for (i = 0; i < numOfDestinations; i++)
                            chromosome[parent1, i] = offSpring1[i];

                    if (offSpring2Fitness < distance[parent2])
                        for (i = 0; i < numOfDestinations; i++)
                            chromosome[parent2, i] = offSpring2[i];

                    for (int p = 0; p < population; p++)
                    {
                        if (distance[p] < minimumDistance)
                        {
                            minimumDistance = distance[p];

                            for (int n = 0; n < numOfDestinations; n++)
                                minimumDistanceResult[n] = chromosome[p, n];
                        }
                    }
                }

                else
                {
                    int i, j, p;
                    int[] child = new int[numOfDestinations];

                    i = random.Next(population);
                    j = random.Next(population);

                    if (distance[i] < distance[j])
                        p = i;

                    else
                        p = j;

                    double childDistance;

                    for (int n = 0; n < numOfDestinations; n++)
                        child[n] = chromosome[p, n];

                    do
                    {
                        i = random.Next(numOfDestinations);
                        j = random.Next(numOfDestinations);
                    }
                    while (i == j);

                    int t = child[i];

                    child[i] = child[j];
                    child[j] = t;

                    childDistance = TourDistance(child);

                    int maxIndex = int.MaxValue;
                    double maxD = double.MinValue;

                    for (int q = 0; q < population; q++)
                    {
                        if (distance[q] >= maxD)
                        {
                            maxIndex = q;
                            maxD = distance[q];
                        }
                    }

                    int[] index = new int[population];
                    int count = 0;

                    for (int q = 0; q < population; q++)
                    {
                        if (distance[q] == maxD)
                        {
                            index[count++] = q;
                        }
                    }

                    maxIndex = index[random.Next(count)];

                    if (childDistance < distance[maxIndex])
                    {
                        distance[maxIndex] = childDistance;

                        for (int n = 0; n < numOfDestinations; n++)
                            chromosome[maxIndex, n] = child[n];

                        if (childDistance < minimumDistance)
                        {
                            minimumDistance = childDistance;

                            for (int n = 0; n < numOfDestinations; n++)
                                minimumDistanceResult[n] = child[n];
                        }
                    }
                }
            }
        }
    }
}