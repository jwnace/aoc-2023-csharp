using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var (workflows, parts) = ParseInput(input);
        var acceptedParts = parts.Where(part => IsPartAccepted(part, workflows["in"], workflows)).ToList();

        return acceptedParts.Sum(part => part.X + part.M + part.A + part.S);
    }

    public static long Solve2(string input)
    {
        var (workflows, _) = ParseInput(input);
        var partRange = new PartRange(1, 4000, 1, 4000, 1, 4000, 1, 4000);
        var acceptedRanges = GetAcceptedRanges(partRange, workflows["in"], workflows);

        return acceptedRanges.Sum(range => range.Product);
    }

    private static (Dictionary<string, Workflow> workflows, IEnumerable<Part> parts) ParseInput(string input)
    {
        var sections = input.Split("\n\n");
        var (workflowStrings, partStrings) = sections.Select(x => x.Split("\n")).ToArray();
        var workflows = workflowStrings.Select(Workflow.Parse).ToDictionary(w => w.Name, w => w);
        var parts = partStrings.Select(Part.Parse);

        return (workflows, parts);
    }

    private static bool IsPartAccepted(Part part, Workflow workflow, IReadOnlyDictionary<string, Workflow> workflows)
    {
        foreach (var rule in workflow.Rules)
        {
            if (rule.Type == RuleType.GoTo)
            {
                return rule.NextWorkflowName switch
                {
                    "A" => true,
                    "R" => false,
                    _ => IsPartAccepted(part, workflows[rule.NextWorkflowName], workflows)
                };
            }

            var value = rule.Category switch
            {
                'x' => part.X,
                'm' => part.M,
                'a' => part.A,
                's' => part.S,
                _ => throw new Exception("Invalid category")
            };

            var satisfiesCondition = rule.Type switch
            {
                RuleType.GreaterThan => value > rule.Threshold,
                RuleType.LessThan => value < rule.Threshold,
                _ => throw new Exception("Invalid rule type")
            };

            if (!satisfiesCondition)
            {
                continue;
            }

            return rule.NextWorkflowName switch
            {
                "A" => true,
                "R" => false,
                _ => IsPartAccepted(part, workflows[rule.NextWorkflowName], workflows)
            };
        }

        return false;
    }

    private static IEnumerable<PartRange> GetAcceptedRanges(
        PartRange startingRange,
        Workflow startingWorkflow,
        IReadOnlyDictionary<string, Workflow> workflows)
    {
        var queue = new Queue<(PartRange, Workflow)>();
        queue.Enqueue((startingRange, startingWorkflow));

        while (queue.Any())
        {
            var (range, workflow) = queue.Dequeue();

            foreach (var rule in workflow.Rules)
            {
                if (rule.Type == RuleType.GoTo)
                {
                    if (rule.NextWorkflowName == "A")
                    {
                        yield return range;
                    }
                    else if (rule.NextWorkflowName != "R")
                    {
                        queue.Enqueue((range, workflows[rule.NextWorkflowName]));
                    }
                }

                if (rule.Category == 'x')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minX = Math.Max(range.MinX, rule.Threshold + 1);
                        var matchingRange = range with { MinX = minX };
                        range = range with { MaxX = minX - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxX = Math.Min(range.MaxX, rule.Threshold - 1);
                        var matchingRange = range with { MaxX = maxX };
                        range = range with { MinX = maxX + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }
                }

                if (rule.Category == 'm')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minM = Math.Max(range.MinM, rule.Threshold + 1);
                        var matchingRange = range with { MinM = minM };
                        range = range with { MaxM = minM - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxM = Math.Min(range.MaxM, rule.Threshold - 1);
                        var matchingRange = range with { MaxM = maxM };
                        range = range with { MinM = maxM + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }
                }

                if (rule.Category == 'a')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minA = Math.Max(range.MinA, rule.Threshold + 1);
                        var matchingRange = range with { MinA = minA };
                        range = range with { MaxA = minA - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxA = Math.Min(range.MaxA, rule.Threshold - 1);
                        var matchingRange = range with { MaxA = maxA };
                        range = range with { MinA = maxA + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }
                }

                if (rule.Category == 's')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minS = Math.Max(range.MinS, rule.Threshold + 1);
                        var matchingRange = range with { MinS = minS };
                        range = range with { MaxS = minS - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxS = Math.Min(range.MaxS, rule.Threshold - 1);
                        var matchingRange = range with { MaxS = maxS };
                        range = range with { MinS = maxS + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                        }
                        else if (rule.NextWorkflowName != "R")
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                        }
                    }
                }
            }
        }
    }
}
