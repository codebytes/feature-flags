using System;
using System.Threading.Tasks;
using Microsoft.FeatureManagement;

public class SundayFeatureFilter : IFeatureFilter
{
    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        bool isEnabled = DateTime.Today.DayOfWeek == DayOfWeek.Sunday;

        return Task.FromResult(isEnabled);
    }
}