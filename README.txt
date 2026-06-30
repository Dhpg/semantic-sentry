SemanticSentry: Deployment & Setup Manual

SemanticSentry is an active zero-trust reverse proxy gateway and real-time security dashboard designed to intercept and deflect context-aware automated reconnaissance traffic at the application edge.

a) Prerequisite Infrastructure
To launch this multi-project solution assembly with full isolation, ensure the target hosting environment has the following software layers installed:

 Docker Desktop / Docker Compose Engine

A modern web browser - Chrome, Edge, Firefox 



b) Local Deployment Orchestration
You can run the entire infrastructure stack using a single orchestrated command line sequence without needing local .NET installations.

1 Open your terminal or PowerShell window inside the root project folder containing your docker-compose.yml file.

2. Execute the build and start sequence in detached background mode:

In PowerShell
docker compose up --build -d

3.   Docker will automatically pull down the slim .NET 9 production runtime layer, compile the solution binaries, mount the local persistence logging directory, and map the communication channels to port 5128.



c) Accessing the Administrative Control Panel
Once the deployment logs report a successful launch, you can access the responsive management console using your local web browser interface:

Dashboard URL: http://localhost:5128/

The browser page uses an integrated background JavaScript polling mechanism to query database updates automatically every 3 seconds, keeping your indicators and logs table perfectly synchronized without manual refreshes.

d) Functional Defense Verification Testing
To verify the active middleware firewall capabilities under real-world request traffic patterns, you can execute these safe command line tests using PowerShell:

Test 1: Authorized Baseline Request
Simulate a safe client navigating through standard application pathways:

In PowerShell
curl.exe http://localhost:5128/

Expected Result: The request receives an immediate HTTP 200 OK network completion state, loading the dashboard elements with zero blocks triggered.

Test 2: Malicious Reconnaissance Probe
Simulate an automated malicious bot scanner attempting to extract sensitive server environment configuration data:

PowerShell
curl.exe http://localhost:5128/.env

Expected Result: The edge gateway middleware flags the payload immediately, terminating downstream thread routing. The terminal receives an HTTP 403 Forbidden response header alongside an explicit security error JSON payload block:

JSON
{"error":"Access Denied by SemanticSentry","reason":"Blocked: Malicious Path Probe Detected","score":0.70}

Dashboard Reaction: Within 3 seconds, the background worker automatically updates the counters on your browser dashboard, populating a red BLOCKED monitoring row tracking the precise transaction footprint.