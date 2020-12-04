defmodule Advent do
  @moduledoc """
  Documentation for `Advent`.
  """

  @doc """
  Hello world.
  """
  def hello do
    :world
  end

  def obtenerRutaArchivo(dia, puzzle) do
    ndiaR = Integer.to_string(dia)
    ndia = if String.length(ndiaR) == 1 do "0#{ndiaR}" else ndiaR end
    "/home/araozu/Programacion/AdventOfCode2020/inputs/input_#{ndia}.txt"
  end

  def main do
    Advent.Dia01.puzzle1(obtenerRutaArchivo(1, 1))
  end
end
