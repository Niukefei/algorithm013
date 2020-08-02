
// 双端队列 - Deque API

Deque<String> deque = new LinkedList<String>();

// --- Thrwos exception 抛出异常 ---
// - first API -
deque.addFirst("a");
deque.addFirst("b");
deque.addFirst("c");
System.out.println(deque);

String firstStr = deque.getFirst();
System.out.println(firstStr);
System.out.println(deque);

while(deque.size() > 0){
    System.out.println(deque.removeFirst());
}
System.out.println(deque);

// - last API -
deque.addLast("d");
deque.addLast("e");
deque.addLast("f");
System.out.println(deque);

String lastStr = deque.getLast();
System.out.println(lastStr);
System.out.println(deque);

while(deque.size() > 0){
    System.out.println(deque.removeLast());
}
System.out.println(deque);

// --- Special value 特殊返回值 ---
// - first API -
deque.offerFirst("g");
deque.offerFirst("h");
deque.offerFirst("i");
System.out.println(deque);

firstStr = deque.peekFirst();
System.out.println(firstStr);
System.out.println(deque);

while(deque.size() > 0){
    System.out.println(deque.pollFirst());
}
System.out.println(deque);

// - last API -
deque.offerLast("j");
deque.offerLast("k");
deque.offerLast("l");
System.out.println(deque);

lastStr = deque.peekLast();
System.out.println(lastStr);
System.out.println(deque);

while(deque.size() > 0){
    System.out.println(deque.pollLast());
}
System.out.println(deque);