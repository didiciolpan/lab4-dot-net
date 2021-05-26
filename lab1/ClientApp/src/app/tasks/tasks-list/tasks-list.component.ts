import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Tasks } from '../tasks.model';
import { TasksService } from '../tasks.service';

@Component({
  selector: 'app-list-tasks',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent {

  public tasks: Tasks[];

  constructor(private tasksService: TasksService) {

  }

  getTasks() {
    this.tasksService.getTasks().subscribe(t => this.tasks = t);
  }

  ngOnInit() {
    this.getTasks();
  }

}
