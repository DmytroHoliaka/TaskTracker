import React from 'react';
import { Droppable } from 'react-beautiful-dnd';
import { ToDoItem } from '../types';
import Task from './Task';

interface ColumnProps {
    title: string;
    tasks: ToDoItem[];
    onDelete: (id: number) => void;
}

const Column: React.FC<ColumnProps> = ({ title, tasks, onDelete }) => {
    return (
        <div className="column">
            <h2>{title}</h2>
            <Droppable droppableId={title}>
                {(provided) => (
                    <div
                        {...provided.droppableProps}
                        ref={provided.innerRef}
                        className="task-list"
                    >
                        {tasks.map((task, index) => (
                            <Task key={task.id} task={task} index={index} onDelete={onDelete} />
                        ))}
                        {provided.placeholder}
                    </div>
                )}
            </Droppable>
        </div>
    );
};

export default Column;
