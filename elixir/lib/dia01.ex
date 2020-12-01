defmodule Advent.Dia01 do
  @moduledoc false

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
    numeros = Enum.map(String.split(str, "\n"), &(String.to_integer(&1)))
    strRes =
      case find2(2020, numeros) do
        {:ok, v1, v2} -> "El resultado es #{v1 * v2}"
        {:err} -> "Numeros no encontrados."
      end
    IO.puts "Dia 01 puzzle 1: #{strRes}"
  end

end
