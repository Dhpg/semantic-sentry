using System;

namespace SemanticSentry.Core
{
    public class ThreatEvaluator
    {
        // Strict threshold: Anything 0.7 or higher gets blocked automatically
        private const double BlockThreshold = 0.7;

        public RequestLog EvaluateRequest(string method, string path, string ip, string token)
        {
            var log = new RequestLog
            {
                Method = method,
                TargetPath = path,
                ClientIp = ip
            };

            // 1. Evaluate Identity Risk (Simulating missing or malformed JWT/API tokens)
            log.IdentityRisk = string.IsNullOrEmpty(token) ? 0.8 : 0.1;

            // 2. Evaluate Anomaly Risk (Simulating standard path scanning attacks)
            log.AnomalyRisk = IsSuspiciousPath(path) ? 0.9 : 0.15;

            // 3. Evaluate Content Risk (Simulating simple payload/query risk)
            log.ContentRisk = method == "POST" || method == "PUT" ? 0.4 : 0.1;

            // Calculate overall risk score using our security formula
            log.FinalSuspicionScore = CalculateFinalScore(log.IdentityRisk, log.AnomalyRisk, log.ContentRisk);

            // Determine mitigation action
            if (log.FinalSuspicionScore >= BlockThreshold)
            {
                log.IsBlocked = true;
                log.MitigationReason = IdentifyPrimaryThreat(log);
            }
            else
            {
                log.IsBlocked = false;
                log.MitigationReason = "Passed";
            }

            return log;
        }

        private double CalculateFinalScore(double identity, double anomaly, double content)
        {
            // Security Formula: Heavily weights anomalies and identity issues
            return (identity * 0.4) + (anomaly * 0.4) + (content * 0.2);
        }

        private bool IsSuspiciousPath(string path)
        {
            var lowerPath = path.ToLower();
            return lowerPath.Contains("/wp-admin") || 
                   lowerPath.Contains("/.env") || 
                   lowerPath.Contains("/config") || 
                   lowerPath.Contains("/etc/passwd");
        }

        private string IdentifyPrimaryThreat(RequestLog log)
        {
            if (log.AnomalyRisk > log.IdentityRisk && log.AnomalyRisk > log.ContentRisk)
                return "Blocked: Malicious Path Probe Detected";
            
            if (log.IdentityRisk > log.AnomalyRisk && log.IdentityRisk > log.ContentRisk)
                return "Blocked: Missing or Invalid Authentication Credentials";

            return "Blocked: High Suspected Aggregate Threat Score";
        }
    }
}