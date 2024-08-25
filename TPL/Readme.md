## Variable Capture

```
for (int i = 0; i < 100; i++)
{
   _ = Task.Run(() => Console.WriteLine(i));
}

```

The output prints multiple instances of "10" instead of the expected values from 0 to 9 because of the behavior of closures in C#. In your code, the Task.Run method captures the variable i, but it does not capture the value of i at the time the lambda expression is created. Instead, it captures the variable itself, meaning all tasks will share the same i.

By the time the tasks actually run, the loop has completed, and i has the value of 10. Since the tasks are run asynchronously, they are executed after the loop has finished, so they all see the final value of i, which is 10.
Task Scheduling: When you call Task.Run, it schedules the task to run asynchronously. However, there's no guarantee that the task will start executing immediately. The task might not start until the loop has completed, especially if the system is busy or the thread pool is handling other tasks.

**Loop Execution Speed:** The loop itself executes very quickly. Even though Task.Run is called 
within the loop, the loop likely completes before any of the tasks get a chance to start.
By the time the tasks start running, the value of i has already incremented to 10.

**Variable Capture:** In C#, the lambda expression captures the variable i by reference, not by value. 
This means that all tasks share the same variable i, and when they eventually run, 
they all see the final value of i, which is 10.

**Threading and Synchronization:** The exact timing of when each task starts depends on the thread scheduler 
and the system's state at runtime. In practice, it's common for the loop to finish entirely before 
any of the tasks have a chance to execute, especially in a simple example like this where the main 
thread is likely much faster than the scheduling and execution of the asynchronous tasks.

### To Summarize:
- Task.Run schedules tasks, but does not guarantee immediate execution.
- The loop runs to completion very quickly, incrementing i up to 10.
- When the tasks eventually run, they all reference the same i, which has reached 10.

## SOLUTION
```
for(int i = 0; i < 10; i++)
{
    int capturedI = i;  // Capture the current value of i
    Task.Run(() =>
    {
        Console.Write(capturedI + " ");
    });
}
```
<p>This way, each task captures a separate copy of the loop variable i, ensuring that they print the correct numbers from 0 to 9.</p>