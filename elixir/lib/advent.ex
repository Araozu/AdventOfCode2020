defmodule Advent do
  @moduledoc """
  Documentation for `Advent`.
  """

  def obtenerRutaArchivo(dia) do
    ndiaR = Integer.to_string(dia)
    ndia = if String.length(ndiaR) == 1 do "0#{ndiaR}" else ndiaR end
    "/home/araozu/Programacion/AdventOfCode2020/inputs/input_#{ndia}.txt"
  end

  def bench(fun) do
    t1 = :os.system_time(:microsecond)
    fun.()
    t2 = :os.system_time(:microsecond)
    IO.puts "Ejecutado en #{t2 - t1} microsegundos."
  end

  def main do
    # Advent.Dia01.puzzle1(obtenerRutaArchivo(1, 1))
    # bench(fn -> Advent.Dia05.puzzle(obtenerRutaArchivo(5)) end)
    bench(fn -> Advent.Dia06.puzzle(obtenerRutaArchivo(6)) end)
  end
end
