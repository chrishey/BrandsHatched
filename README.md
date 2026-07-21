# BrandsHatched

C# implementation of a [circuit breaker pattern] (https://msdn.microsoft.com/en-gb/library/dn589784.aspx).

BrandsHatched is a broken circuit...get it?!?! Nevermind...

## What this is

BrandsHatched is a C# circuit breaker implementation demonstrating the circuit breaker design pattern. It provides a reusable circuit breaker library and a web-based UI (built with Nancy) to visualize and test the circuit breaker's state transitions and behavior in real time.

### Stack

- **Language(s):** C# (97.1%), JavaScript (2.9%)
- **Framework / runtime:** .NET Framework 4.5, Nancy (micro web framework), ASP.NET
- **Notable libraries:** Polly 2.2.1 (resilience library providing circuit breaker primitives), Nancy Razor view engine

## How it's organized

```
src/
  BrandsHatched.CircuitBreaker/     Core circuit breaker library
    CircuitBreaker.cs                Main implementation wrapping Polly
    CircuitBreakerState.cs           Enum: Closed, Open, HalfOpen states
    ICircuitBreaker.cs               Public interface
    Logging/                         Logging abstraction (ILog, Logger)
    Service/                         Example fault-prone service (DumbService)
    Store/                           State persistence (CircuitBreakerStore)
  
  BrandsHatched.Web/                ASP.NET web application
    Modules/                         Nancy HTTP route handlers (HomeModule)
    ViewModel/                       CircuitBreakerModel data transfer object
    Views/                           Razor templates (Home.cshtml, BreakerState.cshtml)
    Web.config                       App config with circuit breaker settings
    packages.config                  Nancy dependencies
```

**How it fits together:** When an HTTP request hits the Nancy web server, `HomeModule` handles routing to endpoints like `/success` and `/failure`. Each endpoint calls `CircuitBreaker.ExecuteAction()`, which uses Polly's circuit breaker policy to decide whether to allow the call to `DumbService`. If the service fails too many times (threshold from Web.config: 2), the circuit "trips" to an Open state. After a wait time (1 minute), it transitions to HalfOpen to test recovery. The `CircuitBreakerStore` tracks state changes and logs them; the UI renders the current state and last state-change timestamp via `CircuitBreakerModel`.

## How to run it

1. Open `src/BrandsHatched.Web/BrandsHatched.Web.sln` in Visual Studio.
2. Build the solution (NuGet will restore Polly and Nancy packages).
3. Run the web project locally (defaults to `http://localhost:<port>`).
4. Click **"Trigger Success"** or **"Trigger Failure"** buttons to exercise the circuit breaker.
   - First 2 failures trip the circuit; subsequent requests are rejected until 1 minute elapses.

State and timestamp are logged to `C:\temp\circuitbreaker.txt` and displayed on screen after each action.
