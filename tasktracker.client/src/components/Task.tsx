import React from 'react';
import axios from 'axios';
import { Draggable } from 'react-beautiful-dnd';
import { ToDoItem } from '../types';

interface TaskProps {
    task: ToDoItem;
    index: number;
    onDelete: (id: number) => void;
}

const Task: React.FC<TaskProps> = ({ task, index, onDelete }) => {
    const handleDelete = async () => {
        await axios.delete(`/api/ToDoItems/${task.id}`);
        onDelete(task.id);
    };

    return (
        <Draggable draggableId={task.id.toString()} index={index}>
            {(provided) => (
                <div
                    className="task"
                    {...provided.draggableProps}
                    {...provided.dragHandleProps}
                    ref={provided.innerRef}
                >
                    <p>{task.title}</p>
                    <button onClick={handleDelete}>Delete</button>
                </div>
            )}
        </Draggable>
    );
};

export default Task;
