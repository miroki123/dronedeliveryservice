using DroneDeliveryService.Entity;
using DroneDeliveryService.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DroneDeliveryService.Service
{
    public class KnapSackService<T> : IKnapSackService<T> where T : IWeight
    {
        /// <summary>
        /// returns the combination of items with the maximum weight capacity and the highest number of items
        /// </summary>
        /// <param name="items">list of possible items</param>
        /// <param name="weightCapacity">the maximum weight capacity to constrain the item combination</param>
        /// <returns></returns>
        public KnapSackResult<T> Solve(T[] items, int weightCapacity)
        {
            int w = weightCapacity;
            int n = items.Length;

            KnapSackResult<T>[,] mat = new KnapSackResult<T>[n + 1, w + 1];
            for (int r = 0; r < w + 1; r++)
            {
                mat[0, r] = new KnapSackResult<T>();
            }
            for (int c = 0; c < n + 1; c++)
            {
                mat[c, 0] = new KnapSackResult<T>();
            }

            for (int item = 1; item <= n; item++)
            {
                for (int capacity = 1; capacity <= w; capacity++)
                {
                    var data = new List<T>();
                    data.AddRange(mat[item - 1, capacity].Items);
                    KnapSackResult<T> maxValWithoutCurr = new KnapSackResult<T> { Items = data };
                    KnapSackResult<T> maxValWithCurr = new KnapSackResult<T>();

                    int weightOfCurr = items[item - 1].Weight;
                    if (capacity >= weightOfCurr)
                    {
                        maxValWithCurr = new KnapSackResult<T> { Items = new List<T> { items[item - 1] } };

                        int remainingCapacity = capacity - weightOfCurr;
                        maxValWithCurr.Items.AddRange(mat[item - 1, remainingCapacity].Items);
                    }

                    mat[item, capacity] = maxValWithCurr.Items.Count > maxValWithoutCurr.Items.Count ? maxValWithCurr : maxValWithoutCurr;
                }
            }

            mat[n, w].UnusedCapacity = weightCapacity - mat[n, w].Items.Sum(x => x.Weight);
            return mat[n, w];
        }
    }
}
