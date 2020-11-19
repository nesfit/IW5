# .NET Internals, IL Generation & Code Analysis

* **HelloWorldGenerator**: Using dnlib library to generate whole assemblies
* **SimpleWeaver**: Using dnlib to add logging to an existing assembly
* **Profiler example**: Contains a simple profiler implementation (written in older C++ standard). This subfolder also contains information on how to build a local version of the CoreCLR, build a profiler and run it together with either the `corerun` or the `dotnet` command.
* **Code analysis**: SharpDetect is available on my gitlab page: [SharpDetect v2](https://gitlab.com/acizmarik/sharpdetect), alternatively you may visit its previous (discontinued) [SharpDetect v1](https://gitlab.com/acizmarik/sharpdetect-1.0) version which provides more information about the tool but does not use the Profiling API yet.
  * This tool is not production-ready and is still in development => use on your own risk
  * **ProducerConsumer.cs**: regular, safe implementation of producer-consumer pattern
* **slides.pptx**: Exported pdf from slides used during presentation

