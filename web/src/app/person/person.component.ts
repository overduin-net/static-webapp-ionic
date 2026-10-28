import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiService, PersonDTO } from 'src/services/generated/api.service';


@Component({
    selector: 'app-person',
    templateUrl: './person.component.html',
    styleUrls: ['./person.component.scss'],
    standalone: false
})

export class PersonComponent implements OnInit {
  message = '';
  errorMessage = '';
  public inputValue = '';
  public emailValue = '';
  public filterValue = '';
  public persons: PersonDTO[] = [];
  public filteredPersons: PersonDTO[] = [];
  public sortColumn: 'id' | 'email' | 'name' = 'id';
  public sortDirection: 'asc' | 'desc' = 'asc';

  constructor(
    private apiService: ApiService,
    private cdr: ChangeDetectorRef
  ) { 
    this.inputValue = '';
   }

  ngOnInit() {
    this.loadPersons();
  }

  getProfile() {
    const trimmedName = this.inputValue.trim();

    if (trimmedName !== '') {
      this.message = '';
      this.errorMessage = '';

      const person = new PersonDTO();
      person.name = trimmedName;
      person.email = this.emailValue.trim();

      this.apiService.createPerson(person).subscribe({
        next: () => {
          this.message = 'Persoon opgeslagen.';
          this.inputValue = '';
          this.emailValue = '';
          this.loadPersons();
          this.cdr.detectChanges();
        },
        error: (error) => {
          const status = error?.status ? ` (${error.status})` : '';
          this.errorMessage = `Opslaan mislukt${status}. Controleer of de API op localhost:7071 draait.`;
        }
      });
    } else {
      this.errorMessage = 'Vul eerst een naam in.';
    }
  }

  public onFilterChange(value: string) {
    this.filterValue = value;
    this.applyFilterAndSort();
  }

  public changeSort(column: 'id' | 'email' | 'name') {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.applyFilterAndSort();
  }

  loadPersons() {
    this.apiService.getAllPersons().subscribe({
      next: (res) => {
        if (res) {
          this.persons = res;
          this.applyFilterAndSort();
          this.cdr.detectChanges();
        }
      },
      error: (error) => {
        const status = error?.status ? ` (${error.status})` : '';
        this.errorMessage = `Laden van personen mislukt${status}.`;
      }
    });
  }

  private applyFilterAndSort() {
    const normalizedFilter = this.filterValue.trim().toLowerCase();

    const filtered = this.persons.filter((person) => {
      if (!normalizedFilter) {
        return true;
      }

      return (`${person.id ?? ''} ${person.email ?? ''} ${person.name ?? ''}`)
        .toLowerCase()
        .includes(normalizedFilter);
    });

    this.filteredPersons = [...filtered].sort((a, b) => {
      const left = `${a[this.sortColumn] ?? ''}`.toLowerCase();
      const right = `${b[this.sortColumn] ?? ''}`.toLowerCase();

      if (left < right) {
        return this.sortDirection === 'asc' ? -1 : 1;
      }

      if (left > right) {
        return this.sortDirection === 'asc' ? 1 : -1;
      }

      return 0;
    });
  }
}
