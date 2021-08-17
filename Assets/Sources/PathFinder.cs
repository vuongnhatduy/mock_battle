using System.Collections.Generic;

public class PathFinder {
    public Trace Run(int[,] matrix, Point start, Point end)
    {
        List<Step> steps = new List<Step>();
        steps.AddRange(findInitSteps(matrix, start));
        List<Trace> tracings = initTracings(steps);
        while (steps.Count != 0)
        {
            Step step = steps[0];
            steps.RemoveAt(0);
            Point stepEnd = move(step, matrix);
            if (stepEnd != null)
            {
                if (stepEnd.Equals(end))
                {
                    return tracings[step.traceId];
                }
                List<Step> movableStep = findMovable(matrix, stepEnd, step);
                steps.AddRange(movableStep);
                tracings[step.traceId].steps.Add(step);
            }
        }
        return null;
    }

    public List<Step> findInitSteps(int[,] matrix, Point p)
    {
        List<Step> moveables = new List<Step>();
        if (p.x + 1 < matrix.GetLength(0) && matrix[p.x + 1, p.y] == 0)
        {
            moveables.Add(new Step(Direction.RIGHT, p.x, p.y));
        }
        if (p.x - 1 >= 0 && matrix[p.x - 1, p.y] == 0)
        {
            moveables.Add(new Step(Direction.LEFT, p.x, p.y));
        }
        if (p.y + 1 < matrix.GetLength(1) && matrix[p.x, p.y + 1] == 0)
        {
            moveables.Add(new Step(Direction.UP, p.x, p.y));
        }
        if (p.y - 1 >= 0 && matrix[p.x, p.y - 1] == 0)
        {
            moveables.Add(new Step(Direction.DOWN, p.x, p.y));
        }
        return moveables;
    }

    public List<Step> findMovable(int[,] matrix, Point p, Step prevStep)
    {
        List<Step> moveables = new List<Step>();
        if (p.x + 1 < matrix.GetLength(0) && matrix[p.x + 1, p.y] == 0 && prevStep.direction != Direction.LEFT)
        {
            moveables.Add(new Step(prevStep.traceId, Direction.RIGHT, p.x, p.y));
        }
        if (p.x - 1 >= 0 && matrix[p.x - 1, p.y] == 0 && prevStep.direction != Direction.RIGHT)
        {
            moveables.Add(new Step(prevStep.traceId, Direction.LEFT, p.x, p.y));
        }
        if (p.y + 1 < matrix.GetLength(1) && matrix[p.x, p.y + 1] == 0 && prevStep.direction != Direction.DOWN)
        {
            moveables.Add(new Step(prevStep.traceId, Direction.UP, p.x, p.y));
        }
        if (p.y - 1 >= 0 && matrix[p.x, p.y - 1] == 0 && prevStep.direction != Direction.UP)
        {
            moveables.Add(new Step(prevStep.traceId, Direction.DOWN, p.x, p.y));
        }
        return moveables;
    }

    public Point move(Step step, int[,] matrix)
    {
        if (step.direction == Direction.LEFT)
        {
            for (int i = step.point.x; i >= 0; i--)
            {
                if (matrix[i, step.point.y] == 1)
                {
                    return new Point(i, step.point.y);
                }
            }
            return new Point(0, step.point.y);
        }
        if (step.direction == Direction.RIGHT)
        {
            for (int i = step.point.x; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, step.point.y] == 1)
                {
                    return new Point(i, step.point.y);
                }
            }
            return new Point(matrix.GetLength(0), step.point.y);
        }
        if (step.direction == Direction.DOWN)
        {
            for (int i = step.point.y; i >= 0; i--)
            {
                if (matrix[step.point.x, i] == 1)
                {
                    return new Point(step.point.x, i);
                }
            }
            return new Point(step.point.x, 0);
        }
        if (step.direction == Direction.UP)
        {
            for (int i = step.point.y; i < matrix.GetLength(1); i++)
            {
                if (matrix[i, step.point.y] == 1)
                {
                    return new Point(step.point.x, i);
                }
            }
            return new Point(step.point.x, matrix.GetLength(1));
        }
        return null;
    }

    public List<Trace> initTracings(List<Step> initSteps)
    {
        List<Trace> traces = new List<Trace>();
        for (int i = 0; i < initSteps.Count; i++)
        {
            Step step = initSteps[i];
            step.traceId = i;
            Trace tr = new Trace();
            tr.id = i;
            tr.steps = new List<Step>();
            tr.steps.Add(step);
            traces.Add(tr);
        }
        return traces;
    }

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    
    public class Point
    {
        public int x;
        public int y;

        public bool Equals(Point b)
        {
            return x == b.x && y == b.y;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Step
    {
        public int traceId;
        public Direction direction;
        public Point point;
        public Step(Direction d, int x, int y)
        {
            this.direction = d;
            this.point = new Point(x, y);
        }

        public Step(int traceId, Direction d, int x, int y)
        {
            this.traceId = traceId;
            this.direction = d;
            this.point = new Point(x, y);
        }
    }

    public class Trace
    {
        public int id;
        public List<Step> steps;
    }
}
