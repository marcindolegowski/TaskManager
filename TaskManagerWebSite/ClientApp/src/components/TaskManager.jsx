import React, { Component } from 'react'
import * as actions from '../actions/index';
import { connect } from 'react-redux';
import { GET_ALL_TASKS, CREATE_TASK, UPDATE_TASK } from '../reducers/index';

const taskStatusMap = {
    '0': 'Open',
    '1': 'Complete'
}

class TaskManager extends Component {
    constructor(props) {
        super(props);
        this.state = {
            newTask: '',
            editedTaskIndex: null,
            editedTaskName: '',
            editedTaskStatus: 0
        }
    }
    componentDidMount() {
        const { getAllTasks } = this.props;
        getAllTasks();
    }

    render() {

        return (
            <div className='container p-2'>
                {this.renderErrorMessage()}

                <div className="form-row align-items-center">
                    <div className="col-auto">
                        <label className="sr-only" htmlFor="inlineFormInput">Task name</label>
                        <input type='text' onChange={this.handleChange} value={this.state.newTask} name='newTask'
                            className="form-control mb-2" id="inlineFormInput" placeholder="Task name"
                        />
                    </div>
                    <div className="col-auto ">
                        <button className='btn btn-primary mb-2' onClick={this.createTask}>Add new task</button>
                    </div>
                    <h3 className='col-auto '>Number of state changes:{this.props.numberOfStateChanges}</h3>
                    <div className="col-auto d-flex justify-content-center">
                        {this.renderLoadingMessage()}
                    </div>
                </div>
                <div className='row'>

                </div>
                <ul className='list-group'>{this.renderTasks()}</ul>
            </div>
        );
    }
    renderErrorMessage = () => {
        const { isError, errorMessage } = this.props;
        return isError ?
            (
                <div className="alert alert-danger" role="alert">
                    {errorMessage}
                </div>
            )
            : null
    }

    renderLoadingMessage = () => {
        const { isLoading } = this.props;
        return isLoading ?
            <div className="d-flex justify-content-center">
                <div className="spinner-border" role="status">
                    <span className="sr-only">Loading...</span>
                </div>
            </div>
            : null;
    }
    handleChange = (e) => {
        const { value, name } = e.target;

        this.setState(prevState => {
            return { [name]: value };
        });
    }
    createTask = () => {
        const { creatTask, getAllTasks } = this.props;

        let request = {
            name: this.state.newTask,
            status: 0
        }
        creatTask(request).then(response => getAllTasks());
        this.setState({ newTask: '' });
    }

    renderTasks = () => {
        const { tasks } = this.props;

        if (tasks) {
            return tasks.map((item, index) => {
                return this.state.editedTaskIndex === index ?
                    this.renderTaskEditor(item, index) :
                    this.renderTask(item, index);
            });
        }
        else {
            return <div>No tasks found</div>
        }
    }

    renderTask = (item, index) => {
        const itemClassName = item.status === 0 ? 'bg-secondary text-dark' : 'bg-success text-white';
        return (
            <li className={`list-group-item my-1 ${itemClassName}`} style={{ padding: '0.25rem 1.25rem' }} key={index}>
                <div className={`row`}>
                    <span className="col-3">{item.name} </span>
                    <span className="col-3">{taskStatusMap[item.status]}</span>
                    <span className="col-5">{item.timeStamp}</span>
                    <div className="col-auto">
                        <button className='btn btn-light' onClick={() => this.updateTask(item, index)}>Edit</button>
                    </div>

                </div>

            </li>
        )
    }

    updateTask = (item, index) => {

        this.setState({
            editedTaskIndex: index,
            editedTaskName: item.name,
            editedTaskStatus: item.status
        });
    }

    renderTaskEditor = (item, index) => {
        return (
            <li className='list-group-item' key={index}>
                <div className="form-row align-items-center">
                    <div className="col-3">
                        <label className="sr-only" htmlFor="inlineFormInput">Task name</label>
                        <input type='text' className="form-control mb-2" id="inlineFormInput"
                            value={this.state.editedTaskName} name='editedTaskName' onChange={this.handleChange} />
                    </div>
                    <div className="col-3">
                        <select className="form-control mb-2 custom-select" value={this.state.editedTaskStatus} onChange={this.handleChange} name='editedTaskStatus'>
                            <option value={0}>{taskStatusMap[0]}</option>
                            <option value={1}>{taskStatusMap[1]}</option>
                        </select>
                    </div>
                    <div className="col-5">
                        <span>{item.timeStamp}</span>
                    </div>
                    <div className="col-auto">
                        <button className='btn btn-primary mb-2' onClick={() => this.submitEdit(item, index)}>Sumbit</button>
                    </div>
                </div>
            </li>
        )
    }

    submitEdit = (item, index) => {
        const { updateTask, getAllTasks } = this.props;

        let request = {
            name: this.state.editedTaskName,
            status: Number(this.state.editedTaskStatus),
            timeStamp: item.timeStamp
        }

        updateTask(request, item.id).then(response => getAllTasks());
        this.setState({
            editedTaskIndex: null,
            editedTaskName: '',
            editedTaskStatus: 0
        });
    }
}

const mapStateToProps = (state) => {
    return state;
};

const mapDispatchToProps = (dispatch) => {
    return {
        getAllTasks: () => dispatch(actions.fetchData({ method: 'GET' }, GET_ALL_TASKS)),
        creatTask: (data) => dispatch(actions.fetchData({ method: 'POST', body: JSON.stringify(data) }, CREATE_TASK)),
        updateTask: (data, id) => dispatch(actions.fetchData({ method: 'PUT', body: JSON.stringify(data) }, UPDATE_TASK, id))
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(TaskManager);;
