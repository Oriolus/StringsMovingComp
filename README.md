# StringsMovingComp

Дан файл (или иной IO источник) со строками, которые необходимо переложить в другие места

Порядок входящих и исходящих строк может не совпадать

В качестве источника используются MemoryStream чтобы не влиял реальный IO

----------------------------------------------------------------------------------------------------------

Входные данные - массив строк, декодированный в массив байт с разделителем 0x00 (RawWordsColumn) 

Эталонная реализация - хранение в виде массива строк (StringHolder). Строка передается через String

Тестируемая реализация - хранение в виде массива байтов + массива с индексами на начало i-й строки в массиве байт (SpanHolder). Строка передается через ReadOnlySnan\<byte\>

Бенчмарк в классе WriteBenchmark

----------------------------------------------------------------------------------------------------------

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1288 (21H1/May2021Update)

Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores

.NET SDK=5.0.402
  [Host]     : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT
  DefaultJob : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT


|                Method | WordsSize |      Mean |    Error |   StdDev | Allocated |
|---------------------- |---------- |----------:|---------:|---------:|----------:|
| WriteWithStringHolder |    500000 |  10.80 ms | 0.132 ms | 0.103 ms |   5,328 B |
|   WriteWithSpanWriter |    500000 |  11.50 ms | 0.013 ms | 0.011 ms |      32 B |
| WriteWithStringHolder |   1000000 |  21.34 ms | 0.152 ms | 0.127 ms |   5,328 B |
|   WriteWithSpanWriter |   1000000 |  22.85 ms | 0.052 ms | 0.048 ms |      32 B |
| WriteWithStringHolder |  10000000 | 214.67 ms | 1.262 ms | 1.181 ms |   5,741 B |
|   WriteWithSpanWriter |  10000000 | 226.81 ms | 0.265 ms | 0.221 ms |   1,384 B |

// * Hints *
Outliers

  WriteBenchmark.WriteWithStringHolder: Default -> 3 outliers were removed (11.26 ms..11.28 ms)
  
  WriteBenchmark.WriteWithSpanWriter: Default   -> 2 outliers were removed, 3 outliers were detected (11.47 ms, 11.52 ms, 11.52 ms)
  
  WriteBenchmark.WriteWithStringHolder: Default -> 2 outliers were removed (22.13 ms, 30.24 ms)
  
  WriteBenchmark.WriteWithSpanWriter: Default   -> 2 outliers were removed (228.18 ms, 228.24 ms)

// * Legends *
  WordsSize : Value of the 'WordsSize' parameter
  
  Mean      : Arithmetic mean of all measurements
  
  Error     : Half of 99.9% confidence interval
  
  StdDev    : Standard deviation of all measurements
  
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  
  1 ms      : 1 Millisecond (0.001 sec)
  
  
----------------------------------------------------------------------------------------------------------

Потребление памяти

![SpanVsStringMemory](https://user-images.githubusercontent.com/23265895/140976184-3351cd5c-fbbf-4ea4-be31-23e64efc8692.JPG) 
