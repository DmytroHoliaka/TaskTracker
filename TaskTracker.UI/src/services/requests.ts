import axios, { AxiosResponse } from "axios";
import TodoItem from "../models/TodoItem";
import parseTodoItems from "./parsers";

export const fetchTasks = async (): Promise<TodoItem[]> => {
  try {
    const response: AxiosResponse<any, any> = await axios.get(
      "https://localhost:7068/api/TodoItems"
    );
    const parsedData: TodoItem[] = parseTodoItems(response.data);
    return parsedData;
  } catch (e) {
    console.error(e);
    return [];
  }
};

export const DeleteTask = async (id: string): Promise<boolean> => {
  try {
    await axios.delete(`https://localhost:7068/api/TodoItems/${id}`);
    return true;
  } catch (e) {
    console.log(e);
    return false;
  }
};

export const CreateTask = async (task: TodoItem): Promise<boolean> => {
  try {
    await axios.post("https://localhost:7068/api/TodoItems", {
      id: task.id,
      title: task.title,
      state: task.state,
    });

    return true;
  } catch (e) {
    console.log(e);
    return false;
  }
};

export const UpdateTask = async (id: string, task: TodoItem): Promise<boolean> => {
  try {
    await axios.put(`https://localhost:7068/api/TodoItems/${id}`, {
      id: task.id,
      title: task.title,
      state: task.state,
    });

    return true;
  } catch (e) {
    return false;
  }
};
