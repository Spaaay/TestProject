import React from 'react';
import axios from 'axios';
import ModalWindow from './modal';
import '../../Content/Table.css';
import styles from '../../Content/Site.css';

export default class Discipline extends React.Component {
	constructor () {
		super();
		this.openModal = this.openModal.bind(this);
		this.afterOpenModal = this.afterOpenModal.bind(this);
		this.closeModal = this.closeModal.bind(this);
		this.setDisciplineName = this.setDisciplineName.bind(this);
        this.setTeacherId = this.setTeacherId.bind(this);
	}

	state = {
		disciplines: [],
		modalIsOpen: false,
		disciplineModel: {
			DisciplineId: undefined,
			DisciplineName: undefined,
            TeacherId: undefined
		},
		isNewDiscipline: false
	};

	blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	height: '100%'
	};

	openModal (item) {
		this.setState({ modalIsOpen: true, disciplineModel: { DisciplineId: item.DisciplineId, DisciplineName: item.DisciplineName, TeacherId: item.TeacherId } });
	}

	afterOpenModal () {
	}

	closeModal (isNeedSave) {
		let root = this;
		this.setState({ modalIsOpen: false });
		if (isNeedSave && this.state.disciplineModel.DisciplineId !== undefined) {
			if (this.state.isNewDiscipline) {
				axios.post('http://localhost:8080/api/discipline/', root.state.disciplineModel)
					.then(function (response) {
						root.setState({ isNewDiscipline: false }, () => { root.getAllDisciplines(); });
					}).catch(function (response) {
					});
			} else {
				axios.put('http://localhost:8080/api/discipline/', root.state.disciplineModel)
					.then(function (response) {
						root.getAllDisciplines();
					}).catch(function (response) {
					});
			}
		}
	}

	componentDidMount (nextProps) {
		this.getAllDisciplines(this);
	}

	deleteDiscipline (id) {
        let root = this;
    	axios.delete('http://localhost:8080/api/discipline/' + id)
    		.then(function (response) {
				root.getAllDisciplines();
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	getAllDisciplines () {
    	let root = this;
    	axios.get('http://localhost:8080/api/discipline')
    		.then(function (response) {
    			let res = JSON.parse(response.data);
    			root.setState({ disciplines: res });
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	setDisciplineName (val) {
		let disc = { ...this.state.disciplineModel };
		disc.DisciplineName = val.target.value;
		this.setState({ disciplineModel: disc });
	}

	setTeacherId (val) {
		let disc = { ...this.state.disciplineModel };
		disc.TeacherId = val.target.value;
		this.setState({ disciplineModel: disc });
    }
    
	renderTableRows (array) {
    	return array.map(item =>
    		<tr key={item.DisciplineId}>
    			<td>{item.DisciplineName}</td>
    			<td>{item.TeacherId}</td>
    			<td><input type='image' name="redact" onClick={() => this.openModal(item)} src="./Content/Images/pencil_and_paper-512.png" width="20"/></td>
    			<td><input type='image' onClick={() => this.deleteDiscipline(item.DisciplineId)} name="delete" src="./Content/Images/filled-trash.png" width="25"/></td>
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
    						<th>Учитель</th>
    					</tr>
    				</thead>
    				<tbody>
    					{this.renderTableRows(this.state.disciplines)}
    				</tbody>
    			</table>
    			<hr/>
    		</div>
			<div style={{ textAlign: 'center' }}>
				<input
					className={styles.addUserInput}
					onClick={() => this.setState({ isNewDiscipline: true }, () => { root.openModal({ DisciplineId: 0, DisciplineName: '', TeacherId: '' }); }) }
					type='image'
					name="addDiscipline"
					src="./Content/Images/add.png"
					width="25">
				</input>
			</div>
			<ModalWindow
				isOpen={this.state.modalIsOpen}
				onAfterOpen={this.afterOpenModal}
				onRequestClose={this.closeModal}
				item={this.state.disciplineModel}
				onClose={this.closeModal}
				modalHeader='Введите данные'
				inputCollection = {[
					{ value: this.state.disciplineModel.DisciplineName, header: 'Имя', onChangeFn: this.setDisciplineName },
					{ value: this.state.disciplineModel.TeacherId, header: 'Учитель', onChangeFn: this.setTeacherId }
				]}
			/>
    	</div>;
	}
}
