defmodule Advent.Dia01 do
  @moduledoc false

  defp find2p(value, numbers) do
    pidActual = self()
    numElems = Enum.count(numbers)
    indices = 0..numElems
    Enum.each(indices, fn i ->
      spawn fn ->
        item = Enum.at(numbers, i)
        nuevosIndices = (i + 1)..numElems

        Enum.each(nuevosIndices, fn j ->
          case Enum.at(numbers, j, nil) do
            nil -> 0
            nItem ->
              if item + nItem == value do
                send(pidActual, {:ok, item, nItem})
              end
          end
        end)

      end
    end)

    receive do
      {:ok, v1, v2} -> IO.puts "Encontrado con procesos: #{v1}, #{v2} -> #{v1 * v2}"
    end
  end

  defp find2(value, numbers) do
    case numbers do
      [x | xs] ->
        case Enum.find(xs, fn v -> x + v == value end) do
          nil -> find2(2020, xs)
          v -> {:ok, x, v}
        end
      [] -> {:err}
    end
  end

  def puzzle1(rutaArchivo) do
    {:ok, str} = File.read(rutaArchivo)
    numeros = Enum.map(String.split(str, "\n"), &(if &1 == "" do 0 else String.to_integer(&1) end))

    # find2p(2020, numeros)

    t1 = :os.system_time(:microsecond)
    strRes =
      case find2(2020, numeros) do
        {:ok, v1, v2} -> "El resultado es #{v1 * v2}"
        {:err} -> "Numeros no encontrados."
      end
    t2 = :os.system_time(:microsecond)
    IO.puts "#{t2 - t1}"
    IO.puts "Dia 01 puzzle 1: #{strRes}"
  end

end
