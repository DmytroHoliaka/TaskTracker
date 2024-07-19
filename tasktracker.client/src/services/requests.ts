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

export const DeleteTask = async (id: number): Promise<void> => {};
