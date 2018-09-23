import React from 'react';
import axios from 'axios';
import ModalWindow from './modal';
import '../../Content/Table.css';
import styles from '../../Content/Site.css';

export default class Schedule extends React.Component {
	constructor () {
		super();
		this.openModal = this.openModal.bind(this);
		this.afterOpenModal = this.afterOpenModal.bind(this);
		this.closeModal = this.closeModal.bind(this);
		this.setScheduleDate = this.setScheduleDate.bind(this);
		this.setScheduleStartTime = this.setScheduleStartTime.bind(this);
		this.setScheduleEndTime = this.setScheduleEndTime.bind(this);
		this.setScheduleGroup = this.setScheduleGroup.bind(this);
		this.setScheduleTeacher = this.setScheduleTeacher.bind(this);
		this.setScheduleDiscipline = this.setScheduleDiscipline.bind(this);

	}

	state = {
		schedule: [],
		modalIsOpen: false,
		scheduleModel: {
			Id: undefined,
			Data: undefined,
			StartTime: undefined,
			EndTime: undefined,
			Teacher: undefined,
			Group: undefined,
			Discipline: undefined
		},
		isNewSchedule: false
	};

	blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	height: '100%'
	};

	openModal (item) {
		this.setState({ modalIsOpen: true, scheduleModel: { Id: item.Id, Data: item.Data, StartTime: item.StartTime, EndTime: item.EndTime, Teacher: item.Teacher ? item.Teacher.FullName : '', Group: item.Group ? item.Group.GroupName : '', Discipline: item.Discipline ? item.Discipline.DisciplineName : '' } });
	}

	afterOpenModal () {
	}

	closeModal (isNeedSave) {
		let root = this;
		this.setState({ modalIsOpen: false });
		if (isNeedSave && this.state.scheduleModel.Id !== undefined) {
			if (this.state.isNewSchedule) {
				axios.post('http://localhost:8080/api/shedule/', root.state.scheduleModel)
					.then(function (response) {
						root.setState({ isNewSchedule: false }, () => { root.getAllSchedules(); });
					}).catch(function (response) {
					});
			} else {
				axios.put('http://localhost:8080/api/shedule/', root.state.scheduleModel)
					.then(function (response) {
						root.getAllSchedules();
					}).catch(function (response) {
					});
			}
		}
	}

	componentDidMount (nextProps) {
		this.getAllSchedules(this);
	}

	deleteSchedule (id) {
		let root = this;
    	axios.delete('http://localhost:8080/api/shedule/' + id)
    		.then(function (response) {
				root.getAllSchedules();
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	getAllSchedules () {
    	let root = this;
    	axios.get('http://localhost:8080/api/shedule')
    		.then(function (response) {
    			let res = JSON.parse(response.data);
    			root.setState({ schedule: res });
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	setScheduleDate (val) {
		let schedule = { ...this.state.scheduleModel };
		schedule.Data = val.target.value;
		this.setState({ scheduleModel: schedule });
	}

	setScheduleStartTime (val) {
		let schedule = { ...this.state.scheduleModel };
		schedule.StartTime = val.target.value;
		this.setState({ scheduleModel: schedule });
	}

    setScheduleEndTime (val) {
    	let schedule = { ...this.state.scheduleModel };
    	schedule.EndTime = val.target.value;
    	this.setState({ scheduleModel: schedule });
    }

    setScheduleTeacher (val) {
    	let schedule = { ...this.state.scheduleModel };
    	schedule.Teacher = val.target.value.FullName;
		this.setState({ scheduleModel: schedule });
    }
    setScheduleGroup (val) {
    	let schedule = { ...this.state.scheduleModel };
    	schedule.Group = val.target.value.GroupName;
    	this.setState({ scheduleModel: schedule });
    }

    setScheduleDiscipline (val) {
    	let schedule = { ...this.state.scheduleModel };
    	schedule.Discipline = val.target.value.DisciplineName;
    	this.setState({ scheduleModel: schedule });
    }

    renderTableRows (array) {
    	return array.map(item =>
    		<tr key={item.Id}>
    			<td>{item.Data}</td>
    			<td>{item.StartTime}</td>
    			<td>{item.EndTime}</td>
				<td>{item.Teacher ? item.Teacher.FullName : ''}</td>
    			<td>{item.Group ? item.Group.GroupName : ''}</td>
    			<td>{item.Discipline ? item.Discipline.DisciplineName : ''}</td>
    			<td><input type='image' name="redact" onClick={() => this.openModal(item)} src="./Content/Images/pencil_and_paper-512.png" width="20"/></td>
    			<td><input type='image' onClick={() => this.deleteSchedule(item.Id)} name="delete" src="./Content/Images/filled-trash.png" width="25"/></td>
    		</tr>
    	);
    }

    render () {
    	let root = this;
    	return <div>
    		<div style={this.blockStyle}>
    			<table>
    				<thead>
    					<tr>
    						<th>Дата</th>
    						<th>Начало</th>
    						<th>Конец</th>
    						<th>Учитель</th>
    						<th>Группа</th>
    						<th>Предмет</th>
    					</tr>
    				</thead>
    				<tbody>
    					{this.renderTableRows(this.state.schedule)}
    				</tbody>
    			</table>
    			<hr/>
    		</div>
    		<div style={{ textAlign: 'center' }}>
    			<input
    				className={styles.addUserInput}
    				onClick={() => this.setState({ isNewSchedule: true }, () => { root.openModal({ Id: 0, Data: '', StartTime: '', EndTime: '', Teacher: '', Group: '', Discipline: '' }); }) }
    				type='image'
    				name="addSchedule"
    				src="./Content/Images/add.png"
    				width="25">
    			</input>
    		</div>
    		<ModalWindow
    			isOpen={this.state.modalIsOpen}
    			onAfterOpen={this.afterOpenModal}
    			onRequestClose={this.closeModal}
    			item={this.state.scheduleModel}
    			onClose={this.closeModal}
    			modalHeader='Введите данные'
    			inputCollection = {[
    				{ value: this.state.scheduleModel.Data, header: 'Дата', onChangeFn: this.setScheduleDate },
    				{ value: this.state.scheduleModel.StartTime, header: 'Начало', onChangeFn: this.setScheduleStartTime },
    				{ value: this.state.scheduleModel.EndTime, header: 'Конец', onChangeFn: this.setScheduleEndTime },
    				{ value: this.state.scheduleModel.Teacher, header: 'Учитель', onChangeFn: this.setScheduleTeacher },
    				{ value: this.state.scheduleModel.Group, header: 'Группа', onChangeFn: this.setScheduleGroup },
    				{ value: this.state.scheduleModel.Discipline, header: 'Предмет', onChangeFn: this.setScheduleDiscipline }
    			]}
    		/>
    	</div>;
    }
}
