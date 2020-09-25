# Kubernetes Presentation

## What is Kubernetes?

Kubernetes is a container orchestrator. It's sole purpose is to help with the process of deploying multiple containers across a fleet of hosts (known as Nodes).

> Kubernetes (K8s) is an open-source system for automating deployment, scaling, and management of containerized applications - k8s website

## Why use Kubernetes?

**Don't... unless you have too!**

Kubernetes is confusing, like really confusing. Setting up a cluster from scratch is incredibly tedious and not recommended!

Use kubernetes if you have any of the following requirements:
- Applications that requires scaling
- Applications that requires service discovery

**And only, ONLY, use Kubernetes if you can use a managed cluster.** This is a cluster managed by the provider (AWS, Azure, GCP) and includes SLA and other guarantees.

If not, use some other simple containerized hosting service like **Azure Web Apps, AWS Elastic Beanstalk or Google Cloud Run**

## What problem does Kubernetes solve?

### Networking

One of the main problems all systems in networking and service discovery.
When deploying new applications, scaling existing ones or deleting applications you continuously have to update services.

This is one of the main issues that Kubernetes solves, it has built in support for:
1. Service discovery
2. Routing
3. Load balancing

### Scaling

### Management

### Telemetry and Logs

## Extensions

### Helm

### k3s vs k8s?!

## Examples



