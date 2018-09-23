import React from 'react';
import axios from 'axios';
import ModalWindow from './modal';
import '../../Content/Table.css';
import styles from '../../Content/Site.css';

export default class Teacher extends React.Component {
	constructor () {
		super();
		this.openModal = this.openModal.bind(this);
		this.afterOpenModal = this.afterOpenModal.bind(this);
		this.closeModal = this.closeModal.bind(this);
		this.setTeacherName = this.setTeacherName.bind(this);
		this.setTeacherPhone = this.setTeacherPhone.bind(this);
	}

	state = {
		teachers: [],
		modalIsOpen: false,
		teacherModel: {
			Id: undefined,
			FullName: undefined,
			Phone: undefined
		},
		isNewTeacher: false
	};

	blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	height: '100%'
	};

	openModal (item) {
		this.setState({ modalIsOpen: true, teacherModel: { Id: item.Id, FullName: item.FullName, Phone: item.Phone } });
	}

	afterOpenModal () {
	}

	closeModal (isNeedSave) {
		let root = this;
		this.setState({ modalIsOpen: false });
		if (isNeedSave && this.state.teacherModel.Id !== undefined) {
			if (this.state.isNewTeacher) {
				axios.post('http://localhost:8080/api/teacher/', root.state.teacherModel)
					.then(function (response) {
						root.setState({ isNewTeacher: false }, () => { root.getAllTeachers(); });
					}).catch(function (response) {
					});
			} else {
				axios.put('http://localhost:8080/api/teacher/', root.state.teacherModel)
					.then(function (response) {
						root.getAllTeachers();
					}).catch(function (response) {
					});
			}
		}
	}

	componentDidMount (nextProps) {
		this.getAllTeachers(this);
	}

	deleteTeacher (id) {
		let root = this;
    	axios.delete('http://localhost:8080/api/teacher/' + id)
    		.then(function (response) {
				root.getAllTeachers();
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	getAllTeachers () {
    	let root = this;
    	axios.get('http://localhost:8080/api/teacher')
    		.then(function (response) {
    			let res = JSON.parse(response.data);
    			root.setState({ teachers: res });
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	setTeacherName (val) {
		let teacher = { ...this.state.teacherModel };
		teacher.FullName = val.target.value;
		this.setState({ teacherModel: teacher });
	}

	setTeacherPhone (val) {
		let teacher = { ...this.state.teacherModel };
		teacher.Phone = val.target.value;
		this.setState({ teacherModel: teacher });
	}

	renderTableRows (array) {
    	return array.map(item =>
    		<tr key={item.Id}>
    			<td>{item.FullName}</td>
    			<td>{item.Phone}</td>
    			<td>{item.Disciplines.map((item, i) => { return item.DisciplineName; }).join(', ')}</td>
    			<td><input type='image' name="redact" onClick={() => this.openModal(item)} src="./Content/Images/pencil_and_paper-512.png" width="20"/></td>
    			<td><input type='image' onClick={() => this.deleteTeacher(item.Id)} name="delete" src="./Content/Images/filled-trash.png" width="25"/></td>
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
    						<th>Имя</th>
    						<th>Телефон</th>
    						<th>Дисциплины</th>
    					</tr>
    				</thead>
    				<tbody>
    					{this.renderTableRows(this.state.teachers)}
    				</tbody>
    			</table>
    			<hr/>
    		</div>
			<div style={{ textAlign: 'center' }}>
				<input
					className={styles.addUserInput}
					onClick={() => this.setState({ isNewTeacher: true }, () => { root.openModal({ Id: 0, FullName: '', Phone: '' }); }) }
					type='image'
					name="addTeacher"
					src="./Content/Images/add.png"
					width="25">
				</input>
			</div>
			<ModalWindow
				isOpen={this.state.modalIsOpen}
				onAfterOpen={this.afterOpenModal}
				onRequestClose={this.closeModal}
				item={this.state.teacherModel}
				onClose={this.closeModal}
				modalHeader='Введите данные преподавателя'
				inputCollection = {[
					{ value: this.state.teacherModel.FullName, header: 'ФИО', onChangeFn: this.setTeacherName },
					{ value: this.state.teacherModel.Phone, header: 'Телефон', onChangeFn: this.setTeacherPhone }
				]}
			/>
    	</div>;
	}
}
