Overview
=======
This package provides two useful classes:
* `Result`: models the result of running an algorithm (such as a validation algorithm) that produces no information other than a sequence of messages
* `Result<T>`: models the result of running an algorithm that produces both an object result and a sequence of messages

Rather than explaining what they do, see the example below.

Usage Example
======
In the following example, we want to validate an instance of `ICar`. 

```csharp
public interface ICar
{
    IEngine Engine { get; }
    ITransmission Transmission { get; }
}

public interface ITransmission
{
    // ...
}

public interface IEngine
{
    // ...
}

public interface ITransmissionValidator
{
    Result Validate(ITransmission transmission);
}

public interface IEngineValidator
{
    Result Validate(IEngine engine);
}

public class CarValidator
{
    private readonly ITransmissionValidator _transmissionValidator;
    private readonly IEngineValidator _engineValidator;

    public CarValidator(ITransmissionValidator transmissionValidator, IEngineValidator engineValidator)
    {
        _transmissionValidator = transmissionValidator;
        _engineValidator = engineValidator;
    }

    // Demonstrates how to produce results.
    public Result Validate(ICar car)
    {
        return
            // Validate the engine and transmission.
            // Using the non-short-circuited and operator (&)
            // will call both validators, and combine the messages
            // from both validators into one Result object.
            _engineValidator.Validate(car.Engine) &
            _transmissionValidator.Validate(car.Transmission);
            
            // If you wanted to only call the second validator if the
            // first one succeeded, you could use the short-circuited
            // and operator (&&).
    }
}

public class CarService {

    private readonly ICarValidator _carValidator;

    public CarService(ICarValidator carValidator){
        _carValidator = carValidator;
    }

    // Demonstrates how to consume Results
    public void Save(ICar car){
        var result = _carValidator.Validate(car);
        
        // You can treat the result like a boolean
        if (!result){
            Console.WriteLine("Car was invalid.");
        }
        
        // And you can access the messages raised as a result of validation.
        foreach (var message in result.Messages){
            Console.WriteLine($"{message.Severity}: {message.Text}");
        }
    }
}
```

See the unit tests for more examples
