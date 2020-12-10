defmodule Advent.Dia10 do
  use Agent

  defp countGap(numbers, {one, three}) do
    case numbers do
      [x | [y | ys]] ->
        new_tuple =
          case y - x do
            3 -> {one, three + 1}
            1 -> {one + 1, three}
            _ -> {one, three}
          end
        countGap([y | ys], new_tuple)
      _ -> {one, three}
    end
  end

  defp test_n_get(numbers, i, agent) do
    res = Agent.get(agent, &(Map.get(&1, i)))
    if res == nil do
      cache = count_paths(numbers, i, agent)
      Agent.update(agent, &(Map.put(&1, i, cache)))
      cache
    else
      res
    end
  end

  defp count_paths(numbers, i, agent) do
    case numbers do
      [x | [y | [z | [a | as]]]] ->
        amount_1 =
          if y - x <= 3 do test_n_get([y | [z | [a | as]]], i + 1, agent)
          else 0 end
        amount_2 =
          if z - x <= 3 do test_n_get([z | [a | as]], i + 2, agent)
          else 0 end
        amount_3 =
          if a - x <= 3 do test_n_get([a | as], i + 3, agent)
          else 0 end

        amount_1 + amount_2 + amount_3
      [x | [y | [z | _]]] ->
        amount_1 = if y - x <= 3 do 1 else 0 end
        amount_2 = if z - x <= 3 do 1 else 0 end

        amount_1 + amount_2
      [_ | [_ | _]] -> 1
      [_ | _] -> 1
      _ -> 0
    end
  end

  def puzzle(file_path) do
    {:ok, str} = File.read(file_path)
    data = String.split(str, "\n")
           |> Enum.map(&(String.to_integer(&1)))
           |> Enum.sort()
    # {one, three} = data |> countGap({0, 0})
    {:ok, agent} = Agent.start_link(fn -> %{} end)
    paths = count_paths(data, 0, agent)

    # IO.puts "One: #{one}, Three: #{three}, Total: #{one * three}"
    IO.puts "Paths: #{paths}"
  end

end
