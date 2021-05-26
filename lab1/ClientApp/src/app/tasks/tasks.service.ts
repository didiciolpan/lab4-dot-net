import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tasks } from './tasks.model';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  getTasks(): Observable<Tasks[]> {
    return this.httpClient.get<Tasks[]>(this.apiUrl + 'tasks');

  }
}
