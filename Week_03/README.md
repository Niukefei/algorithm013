学习笔记
递归模板
// Python 代码模板
def recursion(level, param1, param2, ...):

    # recursion terminator 递归终止条件
    if level > MAX_LEVEL: 
        process_result 
        return 
        
    # process logic in current level 处理当前层逻辑
    process(level, data…) 
    
    # drill down 下探到下一层
    
    self.recursion(level + 1, p1, …) 
    # reverse the current level status if needed 清理当前层

Java 代码模板
public void recur(int level, int param) {
  // terminator
  if (level > MAX_LEVEL) {
    // process result
    return;
  }

  // process current logic
  process(level, param);

  // drill down
  recur( level: level + 1, newParam);

  // restore current status
}

总结：
递归的思想就是，将大问题分解为小问题来求解，然后再将小问题分解为小小问题。这样一层一层地分解，直到问题的数据规模被分解得足够小，不用继续递归分解为止。

分治算法的核心思想其实就是四个字，分而治之 ，也就是将原问题划分成 n个规模较小，并且结构与原问题相似的子问题，递归地解决这些子问题，然后再合并其结果，就得到原问题的解。

分治算法的递归实现中，每一层递归都会涉及这样三个操作：
* 分解：将原问题分解成一系列子问题；
* 解决：递归地求解各个子问题，若子问题足够小，则直接求解；
* 合并：将子问题的结果合并成原问题
