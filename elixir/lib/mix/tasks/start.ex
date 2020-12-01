defmodule Mix.Tasks.Start do
  use Mix.Task

  @shortdoc "Simply calls the Hello.say/0 function."
  def run(_) do
    Mix.Task.run("app.start")
    Advent.main()
  end
end