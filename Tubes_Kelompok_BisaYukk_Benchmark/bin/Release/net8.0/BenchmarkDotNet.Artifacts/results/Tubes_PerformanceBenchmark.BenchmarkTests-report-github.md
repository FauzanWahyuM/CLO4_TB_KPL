```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3775)
12th Gen Intel Core i5-12450HX, 1 CPU, 12 logical and 8 physical cores
.NET SDK 9.0.201
  [Host]     : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX2 [AttachedDebugger]
  Job-IEWSGG : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method                        | Mean       | Error     | StdDev      | Median     | Allocated |
|------------------------------ |-----------:|----------:|------------:|-----------:|----------:|
| TambahTugas                   | 3,113.5 ns | 137.98 ns |   382.33 ns | 3,000.0 ns |     640 B |
| AmbilTugas                    | 1,766.7 ns | 129.15 ns |   378.77 ns | 1,700.0 ns |     432 B |
| AmbilTugasTaskNotFound        |   423.3 ns |  32.72 ns |    89.01 ns |   400.0 ns |     400 B |
| CekTugasTersedia              |   651.6 ns |  74.69 ns |   214.31 ns |   600.0 ns |     400 B |
| HapusTugas                    | 1,513.0 ns | 122.20 ns |   360.32 ns | 1,500.0 ns |     432 B |
| GetSemuaTugas                 | 1,201.0 ns |  49.14 ns |   141.79 ns | 1,200.0 ns |     616 B |
| TambahTugasUntukStatus        | 4,471.7 ns | 283.80 ns |   800.46 ns | 4,350.0 ns |     616 B |
| HapusTugasDariStatus          | 1,394.3 ns |  77.92 ns |   214.61 ns | 1,300.0 ns |     400 B |
| GetTugasDariStatus            | 1,746.9 ns | 165.12 ns |   481.66 ns | 1,700.0 ns |     464 B |
| GetStatusDariTugas            | 5,505.4 ns | 374.75 ns | 1,056.99 ns | 5,100.0 ns |     760 B |
| SimpanJadwal                  | 2,738.9 ns | 229.07 ns |   657.25 ns | 2,400.0 ns |     568 B |
| GetTugasKaryawan              | 3,680.0 ns | 353.60 ns | 1,014.53 ns | 3,300.0 ns |     664 B |
| SelesaikanTugas               | 3,501.0 ns | 278.13 ns |   811.32 ns | 3,150.0 ns |     792 B |
| CekTugasTersimpanTaskNotFound |   529.0 ns |  48.76 ns |   143.76 ns |   500.0 ns |     400 B |
