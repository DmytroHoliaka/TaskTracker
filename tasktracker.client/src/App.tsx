import React, { useEffect, useState } from "react";
import "./App.css";
import InputForm from "./components/InputForm";
import TodoItem from "./models/TodoItem";
import TodoList from "./components/TodoList";
import { DragDropContext, DropResult } from "react-beautiful-dnd";
import { CreateTask, fetchTasks, UpdateTask } from "./services/requests";
import States from "./models/States";

const App: React.FC = () => {
  useEffect(() => {
    const fetchData = async () => {
      let allTasks: TodoItem[] = await fetchTasks();

      const inProgress: TodoItem[] = [];
      const done: TodoItem[] = [];
      const todo: TodoItem[] = [];

      allTasks.forEach((task) => {
        if (task.state === States.Todo) {
          todo.push(task);
        } else if (task.state === States.InProgress) {
          inProgress.push(task);
        } else if (task.state === States.Done) {
          done.push(task);
        }
      });

      setInProgressTasks(inProgress);
      setDoneTasks(done);
      setTodoTasks(todo);
    };

    fetchData();
  }, []);

  const [newTaskTitle, setNewTaskTitle] = useState<string>("");
  const [todoTasks, setTodoTasks] = useState<TodoItem[]>([]);
  const [inProgressTasks, setInProgressTasks] = useState<TodoItem[]>([]);
  const [doneTasks, setDoneTasks] = useState<TodoItem[]>([]);

  const handleCreate = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();

    const newTask: TodoItem = {
      id: crypto.randomUUID(),
      title: newTaskTitle,
      state: States.Todo,
    };

    if (newTaskTitle && (await CreateTask(newTask))) {
      setTodoTasks([...todoTasks, newTask]);
      setNewTaskTitle("");
    }
  };

  const onDragEnd = async (result: DropResult): Promise<void> => {
    const { destination, source } = result;

    if (!destination) {
      return;
    }

    if (
      source.droppableId === destination.droppableId &&
      source.index === destination.index
    ) {
      return;
    }

    let initialTask;
    let newTask;
    let newState;

    let initialTodo = [...todoTasks];
    let initialInProgress = [...inProgressTasks];
    let initialDone = [...doneTasks];

    let todo = [...todoTasks];
    let inProgress = [...inProgressTasks];
    let done = [...doneTasks];

    if (source.droppableId === "TodoBlock") {
      initialTask = todo[source.index];
      todo.splice(source.index, 1);
    } else if (source.droppableId === "InProgressBlock") {
      initialTask = inProgress[source.index];
      inProgress.splice(source.index, 1);
    } else {
      initialTask = done[source.index];
      done.splice(source.index, 1);
    }

    if (destination.droppableId === "TodoBlock") {
      todo.splice(destination.index, 0, initialTask);
      newState = States.Todo;
    } else if (destination.droppableId === "InProgressBlock") {
      inProgress.splice(destination.index, 0, initialTask);
      newState = States.InProgress;
    } else {
      done.splice(destination.index, 0, initialTask);
      newState = States.Done;
    }

    newTask = {
      id: initialTask.id,
      title: initialTask.title,
      state: newState,
    };

    let shouldSwap = true;

    if (initialTask.state !== newTask.state){
      shouldSwap = await UpdateTask(newTask.id, newTask);
    }

    if (shouldSwap) {
      setTodoTasks(todo);
      setInProgressTasks(inProgress);
      setDoneTasks(done);
    } else {
      setTodoTasks(initialTodo);
      setInProgressTasks(initialInProgress);
      setDoneTasks(initialDone);
    }
  };

  return (
    <DragDropContext onDragEnd={onDragEnd}>
      <div className="app">
        <div className="upper-container">
          <div className="header-block">
            <span className="header">Task Tracker</span>
          </div>
          <div className="input-block">
            <InputForm
              taskTitle={newTaskTitle}
              setTaskTitle={setNewTaskTitle}
              handleCreate={handleCreate}
            />
          </div>
        </div>
        <div className="lower-container">
          <TodoList
            todoTasks={todoTasks}
            setTodoTasks={setTodoTasks}
            inProgressTasks={inProgressTasks}
            setInProgressTasks={setInProgressTasks}
            doneTasks={doneTasks}
            setDoneTasks={setDoneTasks}
          />
        </div>
      </div>
    </DragDropContext>
  );
};

export default App;
