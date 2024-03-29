﻿namespace Advent_of_Code_2023.HelperClass
{
    public static class GraphAlgos
    {
        public static (int distance, IEnumerable<T> path) DijkstraToEnd<T>(T start, Func<T, IEnumerable<(T, int)>> getNeighbors, Predicate<T> isEnd)
            where T : notnull
        {
            PriorityQueue<T, int> queue = new();
            queue.Enqueue(start, 0);
            Dictionary<T, (T parent, int distance)> parentsDistances = new();
            parentsDistances[start] = (start, 0);
            while (queue.Count > 0)
            {
                queue.TryDequeue(out var current, out var currentDistance);
                if (parentsDistances[current].distance < currentDistance)
                {
                    continue;
                }
                if (parentsDistances[current].distance > currentDistance)
                {
                    throw new Exception("?");
                }
                if (isEnd(current))
                {
                    IEnumerable<T> GetSteps()
                    {
                        T cursor = current;
                        while (!object.Equals(cursor, start))
                        {
                            yield return cursor;
                            cursor = parentsDistances[cursor].parent;
                        }
                    }
                    return (parentsDistances[current].distance, GetSteps());
                }
                foreach (var (neighbor, distanceToNext) in getNeighbors(current))
                {
                    var nextDistance = currentDistance + distanceToNext;
                    if (!parentsDistances.TryGetValue(neighbor, out var distanceInPD) || nextDistance < distanceInPD.distance)
                    {
                        parentsDistances[neighbor] = (current, nextDistance);
                        queue.Enqueue(neighbor, nextDistance);
                    }
                }
            }
            return (-1, Enumerable.Empty<T>());
        }
    }
}
