import React from "react";
import TodoItem from "../models/TodoItem";
import SingleTodo from "./SingleTodo";
import { Droppable } from "react-beautiful-dnd";

interface Props {
  todoTasks: TodoItem[];
  setTodoTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
  inProgressTasks: TodoItem[];
  setInProgressTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
  doneTasks: TodoItem[];
  setDoneTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
}

const TodoList: React.FC<Props> = ({
  todoTasks,
  setTodoTasks,
  inProgressTasks,
  setInProgressTasks,
  doneTasks,
  setDoneTasks,
}) => {
  return (
    <div className="list-container">
      <Droppable droppableId="TodoBlock">
        {(provider, snapshot) => (
          <div
            ref={provider.innerRef}
            {...provider.droppableProps}
            className={`status-block ${
              snapshot.isDraggingOver ? "drag-over-todo" : ""
            }`}
          >
            <span className="list-title">To do</span>
            <div className="task-block">
              {todoTasks?.map((task, index) => (
                <SingleTodo
                  arrayIndex={index}
                  task={task}
                  tasks={todoTasks}
                  setTasks={setTodoTasks}
                  key={task.id} // System property
                />
              ))}
            </div>
            {provider.placeholder}
          </div>
        )}
      </Droppable>

      <Droppable droppableId="InProgressBlock">
        {(provider, snapshot) => (
          <div
            ref={provider.innerRef}
            {...provider.droppableProps}
            className={`status-block ${
              snapshot.isDraggingOver ? "drag-over-in-progress" : ""
            }`}
          >
            <span className="list-title">In progress</span>
            <div className="task-block">
              {inProgressTasks?.map((task, index) => (
                <SingleTodo
                  arrayIndex={index}
                  task={task}
                  tasks={inProgressTasks}
                  setTasks={setInProgressTasks}
                  key={task.id} // System property
                />
              ))}
            </div>
            {provider.placeholder}
          </div>
        )}
      </Droppable>

      <Droppable droppableId="DoneBlock">
        {(provider, snapshot) => (
          <div
            ref={provider.innerRef}
            {...provider.droppableProps}
            className={`status-block ${
              snapshot.isDraggingOver ? "drag-over-done" : ""
            }`}
          >
            <span className="list-title">Done</span>
            <div className="task-block">
              {doneTasks?.map((task, index) => (
                <SingleTodo
                  arrayIndex={index}
                  task={task}
                  tasks={doneTasks}
                  setTasks={setDoneTasks}
                  key={task.id} // System property
                />
              ))}
            </div>
            {provider.placeholder}
          </div>
        )}
      </Droppable>
    </div>
  );
};

export default TodoList;
