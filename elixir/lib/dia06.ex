defmodule Advent.Dia06 do
  @moduledoc false

  def count_answers(chars, map) do
    case chars do
      [x | xs] ->
        # 10 is the ascii code for '\n'
        new_map = if x == 10 do map else Map.put(map, x, nil) end
        count_answers(xs, new_map)
      [] -> Enum.count(map)
    end
  end

  def split_n_count(str) do
    String.to_charlist(str)
    |> count_answers(%{})
  end

  def get_answers(chars, map) do
    case chars do
      [x | xs] ->
        get_answers(xs, Map.put(map, x, nil))
      [] -> map
    end
  end

  def filter_answers(chars, acc_map, new_map) do
    case chars do
      [x | xs] ->
        if Map.has_key?(acc_map, x) do
          filter_answers(xs, acc_map, Map.put(new_map, x, nil))
        else
          filter_answers(xs, acc_map, new_map)
        end
      [] -> new_map
    end
  end

  def filter_answers_from_string(str, acc_map) do
    filter_answers(String.to_charlist(str), acc_map, %{})
  end

  def split_n_count_2(str) do
    [first_person | rest] = String.split(str, "\n")
    first_persons_answers = get_answers(String.to_charlist(first_person), %{})
    same_answers = List.foldl(rest, first_persons_answers, &(filter_answers_from_string(&1, &2)))
    Enum.count(same_answers)
  end

  def puzzle(file_path) do
    {:ok, str} = File.read(file_path)
    groups = String.split(str, "\n\n")

    total_count =
      Enum.map(groups, &(split_n_count_2(&1)))
      |> Enum.sum()

    IO.puts("Total count: #{total_count}")
  end

end
