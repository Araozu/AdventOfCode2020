defmodule Advent.Dia02 do
  @moduledoc false

  def transformar_a_bits(str) do
    s1 = String.replace(str, ~r/B|R/, "1")
    String.replace(s1, ~r/F|L/, "0")
  end

  def str_a_id(""), do: -1

  def str_a_id(str) do
    bits_string = transformar_a_bits(str)
    {fila_s, columna_s} = String.split_at(bits_string, 7)
    {fila, columna} = {String.to_integer(fila_s, 2), String.to_integer(columna_s, 2)}
    fila * 8 + columna
  end

  def get_id(list) do
    case list do
      [x, y | xs] ->
        if x + 2 == y do
          x + 1
        else
          get_id([y | xs])
        end
      _ -> -1
    end
  end

  def puzzle(rutaArchivo) do
    {:ok, str} = File.read(rutaArchivo)
    max_id = String.split(str, "\n")
             |> Enum.map(&(str_a_id(&1)))
             |> Enum.sort()
             |> get_id()
             # |> Enum.max()

    IO.puts "El id es #{max_id}"
  end

end