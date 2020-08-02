// C#
public class Solution {
    // [26] 删除排序数组中的重复项
    // 双指针法
    public int RemoveDuplicates (int[] nums) {
        if (nums.Length == 0) return 0;
        int j = 0;
        for (int i = 1; i < nums.Length; i++) {
            if (nums[j] != nums[i]) nums[++j] = nums[i];
        }
        return j + 1;
    }

    // [189] 旋转数组
    // 翻转法
    public void Rotate (int[] nums, int k) {
        k %= nums.Length;
        Reverse (nums, 0, nums.Length - 1);
        Reverse (nums, 0, k - 1);
        Reverse (nums, k, nums.Length - 1);
    }
    public void Reverse (int[] nums, int start, int end) {
        while (start < end) {
            nums[start] ^= nums[end];
            nums[end] ^= nums[start];
            nums[start] ^= nums[end];
            start++;
            end--;
        }
    }

    // [21] 合并两个有序链表
    // 递归法
    public ListNode MergeTwoLists (ListNode l1, ListNode l2) {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        if (l1.val < l2.val) {
            l1.next = MergeTwoLists (l1.next, l2);
            return l1;
        } else {
            l2.next = MergeTwoLists (l1, l2.next);
            return l2;
        }
    }

    // [88] 合并两个有序数组
    // 逆向插入法
    public void Merge (int[] nums1, int m, int[] nums2, int n) {
        int i = nums1.Length - 1;
        m--;
        n--;
        while (n >= 0) {
            while (m >= 0 && nums1[m] > nums2[n]) {
                Swap (ref nums1[i--], ref nums1[m--]);
            }
            Swap (ref nums1[i--], ref nums2[n--]);
        }
    }
    public void Swap (ref int x, ref int y) {
        x ^= y;
        y ^= x;
        x ^= y;
    }

    // [1] 两数之和
    // map
    public int[] TwoSum (int[] nums, int target) {
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        int sub;
        for (int i = 0; i < nums.Length; i++) {
            sub = target - nums[i];
            if (dic.ContainsKey (sub)) {
                return new int[] { dic[sub], i };
            }
            dic[nums[i]] = i;
        }
        return null;
    }

    // [283] 移动零
    // 双指针法
    public void MoveZeroes (int[] nums) {
        int j = 0;
        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] != 0) {
                if (j != i) {
                    nums[j] = nums[i];
                    nums[i] = 0;
                }
                j++;
            }
        }
    }

    // [66] 加一
    public int[] PlusOne (int[] digits) {
        int len = digits.Length;
        for (int i = len - 1; i >= 0; i--) {
            digits[i]++;
            digits[i] %= 10;
            if (digits[i] != 0) return digits;
        }
        digits = new int[len + 1];
        digits[0] = 1;
        return digits;
    }

    // [42] 接雨水
    public int Trap (int[] height) {
        int left = 0, right = height.Length - 1;
        int ans = 0;
        int left_max = 0, right_max = 0;
        while (left < right) {
            if (height[left] < height[right]) {
                if (height[left] >= left_max) left_max = height[left];
                else ans += (left_max - height[left]);
                ++left;
            } else {
                if (height[right] >= right_max) right_max = height[right];
                else ans += (right_max - height[right]);
                --right;
            }
        }
        return ans;
    }
}

// [641] 设计循环双端队列
public class MyCircularDeque {
    private int capacity;
    private int[] arr;
    private int front;
    private int rear;

    /** Initialize your data structure here. Set the size of the deque to be k. */
    public MyCircularDeque (int k) {
        capacity = k + 1;
        arr = new int[capacity];
    }

    /** Adds an item at the front of Deque. Return true if the operation is successful. */
    public bool InsertFront (int value) {
        if (IsFull ()) {
            return false;
        }
        front = (front - 1 + capacity) % capacity;
        arr[front] = value;
        return true;
    }

    /** Adds an item at the rear of Deque. Return true if the operation is successful. */
    public bool InsertLast (int value) {
        if (IsFull ()) {
            return false;
        }
        arr[rear] = value;
        rear = (rear + 1) % capacity;
        return true;
    }

    /** Deletes an item from the front of Deque. Return true if the operation is successful. */
    public bool DeleteFront () {
        if (IsEmpty ()) {
            return false;
        }
        front = (front + 1) % capacity;
        return true;
    }

    /** Deletes an item from the rear of Deque. Return true if the operation is successful. */
    public bool DeleteLast () {
        if (IsEmpty ()) {
            return false;
        }
        // rear 被设计在数组的末尾，所以是 -1
        rear = (rear - 1 + capacity) % capacity;
        return true;
    }

    /** Get the front item from the deque. */
    public int GetFront () {
        if (IsEmpty ()) {
            return -1;
        }
        return arr[front];
    }

    /** Get the last item from the deque. */
    public int GetRear () {
        if (IsEmpty ()) {
            return -1;
        }
        return arr[(rear - 1 + capacity) % capacity];
    }

    /** Checks whether the circular deque is empty or not. */
    public bool IsEmpty () {
        return front == rear;
    }

    /** Checks whether the circular deque is full or not. */
    public bool IsFull () {
        return (rear + 1) % capacity == front;
    }
}