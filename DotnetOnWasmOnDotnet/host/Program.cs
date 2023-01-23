
string input = "/etc/hosts";
string output = "hosts";

Wasmtime.WasiConfiguration conf = new Wasmtime.WasiConfiguration()
    .WithEnvironmentVariable("input-file", input)
    .WithEnvironmentVariable("output-file", output)
    // .WithPreopenedDirectory("/etc", "/etc")
    // .WithPreopenedDirectory(".", ".")
    .WithInheritedStandardOutput();

var engineConfig = new Wasmtime.Config();
var engine = new Wasmtime.Engine(engineConfig);
var linker = new Wasmtime.Linker(engine);
linker.DefineWasi();
var store = new Wasmtime.Store(engine);
store.SetWasiConfiguration(conf);

string wasm = @"../guest/bin/Debug/net7.0/guest.wasm";
var module = Wasmtime.Module.FromFile(engine, wasm);
var inst = linker.Instantiate(store, module);
var start = inst.GetFunction("_start");

Console.WriteLine("Host executing...");

try
{
    if (start != null)
        start.Invoke();
}
catch (Wasmtime.WasmtimeException we)
{
    Console.WriteLine(we);
}