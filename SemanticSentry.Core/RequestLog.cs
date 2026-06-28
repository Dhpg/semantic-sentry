using System;

namespace SemanticSentry.Core
{
    public class RequestLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Method { get; set; } = "GET";
        public string TargetPath { get; set; } = "/";
        public string ClientIp { get; set; } = "127.0.0.1";
        
        // Threat formula inputs
        public double IdentityRisk { get; set; }
        public double AnomalyRisk { get; set; }
        public double ContentRisk { get; set; }
        
        // Calculated final score outputs
        public double FinalSuspicionScore { get; set; }
        public bool IsBlocked { get; set; }
        public string MitigationReason { get; set; } = "Passed";
    }
}