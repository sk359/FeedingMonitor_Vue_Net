export async function get(url: string) {
  const response = await fetch(url, { method: 'get', headers: {'Access-Control-Allow-Origin': '*'}})
  const data = await response.json()
  return data
}

export async function post(url: string, data: any) {
  const response = await fetch(url, {method: 'post', headers: {'Content-Type': 'application/json'}, body: JSON.stringify(data)})
  const responseData = await response.json()
  return responseData
}

export async function deleteById(url: string) {
  const response = await fetch(url, {method: 'delete'})
  const responseData = await response.json()
  return responseData
}
