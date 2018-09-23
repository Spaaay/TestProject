import React from 'react';
import { render } from 'react-dom';
import Teacher from './teachers';
import Schedule from './Schedule';
import Group from './groups';
import User from './users';
import Discipline from './disciplines';
import styles from '../../Content/Site.css';
import ModalWindow from './modal';
import axios from 'axios';

class App extends React.Component {
	state = {
		isTeacherBlockShow: false,
		isScheduleBlockShow: false,
		isGroupBlockShow: false,
		isUserBlockShow: false,
		isDisciplineBlockShow: false,
		isAccessDenied: true,
		modalIsOpen: false,
		userInfo: {
			isAuthenticated: false,
			login: undefined,
			password: undefined,
			userRole: undefined
		}
	}

	componentDidMount () {
		this.openModal();
	}

	openModal () {
		this.setState({ modalIsOpen: true });
	}

	setUserLogin = (val) => {
		let user = { ...this.state.userInfo };
		user.login = val.target.value;
		this.setState({ userInfo: user });
	}

	setUserPassword = (val) => {
		let user = { ...this.state.userInfo };
		user.password = val.target.value;
		this.setState({ userInfo: user });
	}

	closeModal = (isNeedSave) => {
		let root = this;
		// this.setState({ modalIsOpen: false });
		axios.post('http://localhost:8080/api/authorization/', root.state.userInfo)
			.then(function (response) {
				let user = JSON.parse(response.data);
				if (user != undefined && user != null) {
					root.setState({ modalIsOpen: false, isAccessDenied: false });
				} else {
					root.setState({ modalIsOpen: true, isAccessDenied: true });
				}				
			}).catch(function (response) {
			});
	}

	render () {
		return <div>
			{this.state.isAccessDenied && <ModalWindow
				isOpen={this.state.modalIsOpen}
				onAfterOpen={this.afterOpenModal}
				onRequestClose={this.closeModal}
				item={this.state.teacherModel}
				onClose={this.closeModal}
				modalHeader='войдите в систему'
				isSignBtn={true}
				inputCollection = {[
					{ value: this.state.userInfo.login, header: 'логин', onChangeFn: this.setUserLogin },
					{ value: this.state.userInfo.pass, header: 'пароль', onChangeFn: this.setUserPassword }
				]}

			/>}
			{!this.state.isAccessDenied && <div>
				<div className='Containter'>
					<div className={styles.mainBlock} onClick={() => this.showTeacher()}>
						<p>Преподаватели</p>
					</div>
					{this.state.isTeacherBlockShow && <Teacher/>}
				</div>
				<div className='Containter'>
					<div className={styles.mainBlock} onClick={() => this.showSchedule()}>
						<p>Расписание</p>
					</div>
					{this.state.isScheduleBlockShow && <Schedule/>}
				</div>
				<div className='Containter'>
					<div className={styles.mainBlock} onClick={() => this.showGroup()}>
						<p>Группы</p>
					</div>
					{this.state.isGroupBlockShow && <Group/>}
				</div>
				<div className='Containter'>
					<div className={styles.mainBlock} onClick={() => this.showUser()}>
						<p>Пользователи</p>
					</div>
					{this.state.isUserBlockShow && <User/>}
				</div>			
				<div className='Containter'>
					<div className={styles.mainBlock} onClick={() => this.showDiscipline()}>
						<p>Предметы</p>
					</div>
					{this.state.isDisciplineBlockShow && <Discipline/>}
				</div>



				</div>}
		</div>;
	}

	showTeacher () {
		if (this.state.isTeacherBlockShow) {
			this.setState({ isTeacherBlockShow: false });
		} else {
			this.setState({ isTeacherBlockShow: true });
		}
	}
	showSchedule () {
		if (this.state.isScheduleBlockShow) {
			this.setState({ isScheduleBlockShow: false });
		} else {
			this.setState({ isScheduleBlockShow: true });
		}
	}
	showGroup () {
		if (this.state.isGroupBlockShow) {
			this.setState({ isGroupBlockShow: false });
		} else {
			this.setState({ isGroupBlockShow: true });
		}
	}
	showUser () {
		if (this.state.isUserBlockShow) {
			this.setState({ isUserBlockShow: false });
		} else {
			this.setState({ isUserBlockShow: true });
		}
	}
	showDiscipline () {
		if (this.state.isDisciplineBlockShow) {
			this.setState({ isDisciplineBlockShow: false });
		} else {
			this.setState({ isDisciplineBlockShow: true });
		}
	}
}

render(<App/>, document.getElementById('app'));
