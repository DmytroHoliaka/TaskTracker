import States from "./States"

interface TodoItem{
    id: string;
    title: string;
    state: States
}

export default TodoItem;