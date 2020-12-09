package dia08

class Computer(private val list: DoublyLinkedList<Instruction>) {

    var accumulator = 0

    private fun doACC(value: Int): Instruction {
        accumulator += value
        return list.getNext() ?: throw Error("Next instruction was null. Acc value is $accumulator")
    }

    private fun doJMP(value: Int): Instruction {
        val returnValue = if (value < 0) {
            list.getBack(kotlin.math.abs(value))
        } else {
            list.getNext(value)
        }

        return returnValue ?: throw Error("Next instruction was null. Acc value is $accumulator")
    }

    private fun doNOP(): Instruction {
        return list.getNext() ?: throw Error("Next instruction was null.. Acc value is $accumulator")
    }

    fun runPuzzle1() {
        accumulator = 0
        list.resetCurrent()
        var nextInstruction = list.getCurrent()

        while (nextInstruction.counter < 1) {
            nextInstruction.visit()
            nextInstruction = when (nextInstruction) {
                is ACC -> doACC(nextInstruction.value)
                is JMP -> doJMP(nextInstruction.value)
                is NOP -> doNOP()
            }
        }

        println("The accumulator's value is: $accumulator")
    }

    fun runPuzzle2() {

        var instructionChagedPos = 1
        while (instructionChagedPos < list.length) {
            accumulator = 0
            list.foreach { it.resetCounter() }
            list.resetCurrent()

            var instructionsRemaining = instructionChagedPos
            var nextInstruction = list.getCurrent()

            var instructionChanged: Instruction? = null

            while (nextInstruction.counter < 1) {
                nextInstruction.visit()
                nextInstruction = when (nextInstruction) {
                    is ACC -> doACC(nextInstruction.value)
                    is JMP -> {
                        instructionsRemaining--
                        if (instructionsRemaining == 0) {
                            instructionChanged = nextInstruction
                            doNOP()
                        } else {
                            if (instructionChanged != null && nextInstruction === instructionChanged) {
                                doNOP()
                            } else {
                                doJMP(nextInstruction.value)
                            }
                        }
                    }
                    is NOP -> {
                        instructionsRemaining--
                        if (instructionsRemaining == 0) {
                            instructionChanged = nextInstruction
                            doJMP(nextInstruction.value)
                        } else {
                            if (instructionChanged != null && nextInstruction === instructionChanged) {
                                doJMP(nextInstruction.value)
                            } else {
                                doNOP()
                            }
                        }
                    }
                }
            }

            instructionChagedPos++

        }

        println("Accumulator is $accumulator")
    }

}
