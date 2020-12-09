package dia08

import kotlin.math.abs

class Node<T>(val value: T) {
    var previous: Node<T>? = null
    var next: Node<T>? = null
}

class DoublyLinkedList<T> {

    lateinit var head: Node<T>
    lateinit var tail: Node<T>
    lateinit var current: Node<T>
    var currentPosition = 0
    var length = 0

    fun insert(value: T) {
        val newNode = Node(value)

        if (length == 0) {
            head = newNode
            tail = newNode
            current = newNode
            currentPosition = 0
        } else {
            tail.next = newNode
            newNode.previous = tail
            tail = newNode
        }

        length++
    }

    fun getCurrent(): T = current.value

    fun getNext(): T? {
        current = current.next ?: return null
        val returnValue = current.value
        currentPosition++
        return returnValue
    }

    fun getBack(): T? {
        val returnValue = current.previous?.value
        return if (returnValue == null) {
            null
        } else {
            currentPosition--
            returnValue
        }
    }

    fun getNext(amount: Int): T? {
        for (i in 1..amount) {
            current = current.next ?: return null
            currentPosition++
        }

        return current.value
    }

    fun getBack(amount: Int): T? {
        for (i in 1..amount) {
            current = current.previous ?: return null
            currentPosition--
        }

        return current.value
    }

    operator fun get(position: Int): T? {
        if (position < 0 || position > length) return null

        var (startingNode, changeRate, distance) =
            if (position == currentPosition) return current.value
            // It's faster to go through the head
            else if (position < currentPosition - position) {
                Triple(head, 1, position)
            }
            // It's faster to go through the current node
            else if (abs(currentPosition - position) < position && abs(currentPosition - position) < length - position) {
                Triple(current, if (currentPosition < position) 1 else -1, abs(currentPosition - position))
            }
            // It's faster to go through the tail
            else {
                Triple(tail, -1, length - position)
            }

        for (i in 0..distance) {
            // Go back
            startingNode = if (changeRate < 0) {
                startingNode.previous ?: return null
            } else {
                startingNode.next ?: return null
            }
        }

        current = startingNode
        currentPosition = position

        return startingNode.value
    }

    fun resetCurrent() {
        current = head
        currentPosition = 0
    }

    fun foreach(f: (T) -> Unit) {
        if (length == 0) return

        var next: Node<T>? = head
        while (next != null) {
            f(next.value)
            next = next.next
        }
    }

}
